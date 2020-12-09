using System.Collections.Generic;
using IntcodeComputer.Operations.Contracts;

namespace IntcodeComputer.Operations
{
    public class Multiply : IMemoryOperation
    {
        public static Multiply I => new Multiply();
        public int Inputs => 2;
        public int Outputs => 1;

        public void Exec(List<long> memory, long[] inputs, int[] outAddr)
        {
            memory[outAddr[0]] = inputs[0] * inputs[1];
        }
    }
}