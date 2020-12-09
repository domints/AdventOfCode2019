using System.Collections.Generic;
using IntcodeComputer.Operations.Contracts;

namespace IntcodeComputer.Operations
{
    public class LessThan : IMemoryOperation
    {
        public static LessThan I => new LessThan();
        public int Inputs => 2;
        public int Outputs => 1;

        public void Exec(List<long> memory, long[] inputs, int[] outAddr)
        {
            memory[outAddr[0]] = inputs[0] < inputs[1] ? 1 : 0;
        }
    }
}