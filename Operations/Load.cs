using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using AdventOfCode2019.Operations.Contracts;

namespace AdventOfCode2019.Operations
{
    public class Load : IMemoryOperation
    {
        private readonly ConcurrentQueue<long> inputBuffer;

        public static Load I(ConcurrentQueue<long> inputBuffer) => new Load(inputBuffer);

        private Load() { }
        private Load(ConcurrentQueue<long> inputBuffer)
        {
            this.inputBuffer = inputBuffer;
        }

        public int Inputs => 0;
        public int Outputs => 1;

        public void Exec(List<long> memory, long[] inputs, int[] outAddr)
        {
            var success = false;
            while(!success)
            {
                Thread.Sleep(1);
                success = inputBuffer.TryDequeue(out long value);
                if(success) memory[outAddr[0]] = value;
            }
        }
    }
}