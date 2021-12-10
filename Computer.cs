using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Operations;
using AdventOfCode2019.Operations.Contracts;

namespace AdventOfCode2019
{
    public class Computer
    {
        private const int HaltOpCode = 99;

        private readonly List<long> memory;
        private ConcurrentQueue<long> inputBuffer;
        private Action<long> outputMethod;
        private readonly ConcurrentQueue<long> outputBuffer;
        private List<long> outputBlock;
        private int counter = 0;
        private int relativeOffset = 0;
        public List<long> Memory => memory;
        public ConcurrentQueue<long> InputBuffer => inputBuffer;
        public ConcurrentQueue<long> OutputBuffer => outputBuffer;
        public int ProgramCounter => counter;

        Dictionary<int, IOperation> operations;

        public Computer(string fileName)
        {
            memory = System.IO.File.ReadAllText(fileName)
                .Split(',')
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => long.Parse(l))
                .ToList();
            
            outputBuffer = new ConcurrentQueue<long>();
        }

        public Computer(IEnumerable<long> memoryInput)
        {
            memory = new List<long>(memoryInput);
            outputBuffer = new ConcurrentQueue<long>();
        }

        public void Run(ConcurrentQueue<long> inputBuffer, int outputBlockLength, Action<List<long>> outputMethod)
        {
            outputBlock = new List<long>();
            Run(inputBuffer, (val) => LoadOutBlock(outputBlockLength, val, outputMethod));
        }

        public void Run(ConcurrentQueue<long> inputBuffer = null, Action<long> outputMethod = null)
        {
            this.inputBuffer = inputBuffer;
            this.outputMethod = outputMethod ?? (v => { });

            operations = new Dictionary<int, IOperation>
            {
                [1] = Add.I,
                [2] = Multiply.I,
                [3] = Load.I(inputBuffer),
                [4] = Store.I(ProcessStore),
                [5] = JumpIfTrue.I,
                [6] = JumpIfFalse.I,
                [7] = LessThan.I,
                [8] = IsEqual.I,
                [9] = ChangeRelativeIndex.I
            };

            while (Process()) ;
        }

        bool Process()
        {
            var instruction = memory[counter].ToString();
            var OPcode = int.Parse(instruction.Substring(Math.Max(instruction.Length - 2, 0)));
            if (OPcode == HaltOpCode) return false;

            var paramModes = instruction.Substring(0, Math.Max(instruction.Length - 2, 0));

            if (!operations.TryGetValue(OPcode, out IOperation op))
                throw new InvalidOperationException("Unknown OPcode!");

            Exec(op, paramModes);

            return true;
        }

        (long[] inputs, int[] outAddr) GetParameters(IOperation operation, string paramModes)
        {
            var modes = paramModes.ToCharArray();
            Array.Reverse(modes);
            long[] inps = new long[operation.Inputs];
            for (int i = 0; i < operation.Inputs; i++)
            {
                int inputAddr = 0;
                switch (modes[i])
                {
                    case '0':
                        inputAddr = (int)memory[counter + 1 + i];
                        break;
                    case '1':
                        inputAddr = counter + 1 + i;
                        break;
                    case '2': 
                        inputAddr = (int)memory[counter + 1 + i] + relativeOffset;
                        break;
                }

                if(inputAddr > memory.Count - 1)
                {
                    memory.AddRange(
                        Enumerable.Range(0, inputAddr - memory.Count + 2).Select(_ => 0L)
                    );
                }

                inps[i] = memory[inputAddr];
            }

            int[] outs = new int[operation.Outputs];
            for (int i = 0; i < operation.Outputs; i++)
            {
                int outputAddr = 0;
                if(modes[operation.Inputs + i] == '0')
                {
                    outputAddr = (int)memory[counter + operation.Inputs + i + 1];
                }
                else if(modes[operation.Inputs + i] == '2')
                {
                    outputAddr = (int)memory[counter + operation.Inputs + i + 1] + relativeOffset;
                }

                if(outputAddr > memory.Count - 1)
                {
                    memory.AddRange(
                        Enumerable.Range(0, outputAddr - memory.Count + 2).Select(_ => 0L)
                    );
                }

                if(outputAddr < 0) throw new InvalidOperationException("Output address cannot be negative!"); 

                outs[i] = outputAddr;
            }

            return (inps, outs);
        }

        void Exec(IOperation operation, string paramModes)
        {
            var (ins, outs) = GetParameters(operation, paramModes.PadLeft(operation.Inputs + operation.Outputs, '0'));
            var newPointer = -1;
            if(operation is IJumpOperation jumpOp)
            {
                newPointer = jumpOp.Exec(memory, ins);
            }
            else if(operation is IMemoryOperation memOp)
            {
                memOp.Exec(memory, ins, outs);
            }
            else if(operation is IRelativeAdjustOperation relOp)
            {
                relativeOffset = relOp.Exec(ins, relativeOffset);
            }

            if(newPointer == -1)
            {
                counter += operation.Inputs + operation.Outputs + 1;
            }
            else
                counter = newPointer;
        }

        void ProcessStore(long value)
        {
            outputBuffer.Enqueue(value);
            outputMethod?.Invoke(value);
        }

        void LoadOutBlock(int length, long value, Action<List<long>> outAction)
        {
            outputBlock.Add(value);
            if(outputBlock.Count == length)
            {
                outAction(outputBlock);
                outputBlock = new List<long>();
            }
        }
    }
}