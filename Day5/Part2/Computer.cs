using System;
using System.Collections.Generic;

namespace Day5
{
    public class Computer
    {
        private readonly List<int> memory;
        private Queue<int> inputBuffer;
        private Action<int> outputMethod;
        private readonly Queue<int> outputBuffer;
        private int counter = 0;
        public List<int> Memory => memory;
        public Queue<int> InputBuffer => inputBuffer;
        public Queue<int> OutputBuffer => outputBuffer;
        public int ProgramCounter => counter;

        Dictionary<int, IOperation> operations;

        public Computer(List<int> memoryInput)
        {
            memory = new List<int>(memoryInput);
            this.outputBuffer = new Queue<int>();
        }

        public void Run(Queue<int> inputBuffer = null, Action<int> outputMethod = null)
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
                [8] = IsEqual.I
            };

            while (Process()) ;
        }

        bool Process()
        {
            var instruction = memory[counter].ToString();
            var OPcode = int.Parse(instruction.Substring(Math.Max(instruction.Length - 2, 0)));
            if (OPcode == 99) return false;

            var paramModes = instruction.Substring(0, Math.Max(instruction.Length - 2, 0));

            if (!operations.TryGetValue(OPcode, out IOperation op))
                throw new InvalidOperationException("Unknown OPcode!");

            Exec(op, paramModes);

            return true;
        }

        (int[] inputs, int[] outAddr) GetParameters(IOperation operation, string paramModes)
        {
            var modes = paramModes.ToCharArray();
            Array.Reverse(modes);
            int[] inps = new int[operation.Inputs];
            for (int i = 0; i < operation.Inputs; i++)
            {
                switch (modes[i])
                {
                    case '0':
                        inps[i] = memory[memory[counter + 1 + i]];
                        break;
                    case '1':
                        inps[i] = memory[counter + 1 + i];
                        break;
                }
            }

            int[] outs = new int[operation.Outputs];
            for (int i = 0; i < operation.Outputs; i++)
            {
                outs[i] = memory[counter + operation.Inputs + i + 1];
            }

            return (inps, outs);
        }

        void Exec(IOperation operation, string paramModes)
        {
            var (ins, outs) = GetParameters(operation, paramModes.PadLeft(operation.Inputs + operation.Outputs, '0'));
            var newPointer = -1;
            if(operation is IJumpOperation jumpOp)
            {
                newPointer = jumpOp.Exec(memory, ins, outs);
            }
            else if(operation is IMemoryOperation memOp)
            {
                memOp.Exec(memory, ins, outs);
            }

            if(newPointer == -1)
            {
                counter += operation.Inputs + operation.Outputs + 1;
            }
            else
                counter = newPointer;
        }

        void ProcessStore(int value)
        {
            outputBuffer.Enqueue(value);
            outputMethod?.Invoke(value);
        }
    }

    interface IOperation
    {
        int Inputs { get; }
        int Outputs { get; }
    }

    interface IMemoryOperation : IOperation
    {
        void Exec(List<int> memory, int[] inputs, int[] outputAddr);
    }

    interface IJumpOperation : IOperation
    {
        int Exec(List<int> memory, int[] inputs, int[] outputAddr);
    }

    class Add : IMemoryOperation
    {
        public static Add I => new Add();

        public int Inputs => 2;
        public int Outputs => 1;

        public void Exec(List<int> memory, int[] inputs, int[] outAddr)
        {
            memory[outAddr[0]] = inputs[0] + inputs[1];
        }
    }

    class Multiply : IMemoryOperation
    {
        public static Multiply I => new Multiply();
        public int Inputs => 2;
        public int Outputs => 1;

        public void Exec(List<int> memory, int[] inputs, int[] outAddr)
        {
            memory[outAddr[0]] = inputs[0] * inputs[1];
        }
    }

    class Load : IMemoryOperation
    {
        private readonly Queue<int> inputBuffer;

        public static Load I(Queue<int> inputBuffer) => new Load(inputBuffer);

        private Load() { }
        private Load(Queue<int> inputBuffer)
        {
            this.inputBuffer = inputBuffer;
        }

        public int Inputs => 0;
        public int Outputs => 1;

        public void Exec(List<int> memory, int[] inputs, int[] outAddr)
        {
            System.Diagnostics.Debugger.Break();
            memory[outAddr[0]] = inputBuffer.Dequeue();
        }
    }

    class Store : IMemoryOperation
    {
        public static Store I(Action<int> storeAction) => new Store(storeAction);
        private readonly Action<int> storeAction;

        private Store() { }
        private Store(Action<int> storeAction)
        {
            this.storeAction = storeAction;
        }

        public int Inputs => 1;
        public int Outputs => 0;

        public void Exec(List<int> memory, int[] inputs, int[] outAddr)
        {
            storeAction(inputs[0]);
        }
    }

    class JumpIfTrue : IJumpOperation
    {
        public static JumpIfTrue I => new JumpIfTrue();
        public int Inputs => 2;
        public int Outputs => 0;

        public int Exec(List<int> memory, int[] inputs, int[] outAddr)
        {
            return inputs[0] == 0 ? -1 : inputs[1];
        }
    }

    class JumpIfFalse : IJumpOperation
    {
        public static JumpIfFalse I => new JumpIfFalse();
        public int Inputs => 2;
        public int Outputs => 0;

        public int Exec(List<int> memory, int[] inputs, int[] outAddr)
        {
            return inputs[0] == 0 ? inputs[1] : -1;
        }
    }

    class LessThan : IMemoryOperation
    {
        public static LessThan I => new LessThan();
        public int Inputs => 2;
        public int Outputs => 1;

        public void Exec(List<int> memory, int[] inputs, int[] outAddr)
        {
            memory[outAddr[0]] = inputs[0] < inputs[1] ? 1 : 0;
        }
    }

    class IsEqual : IMemoryOperation
    {
        public static IsEqual I => new IsEqual();
        public int Inputs => 2;
        public int Outputs => 1;

        public void Exec(List<int> memory, int[] inputs, int[] outAddr)
        {
            memory[outAddr[0]] = inputs[0] == inputs[1] ? 1 : 0;
        }
    }
}