using System.Collections.Generic;
using AdventOfCode2019.Operations.Contracts;

namespace AdventOfCode2019.Operations
{
    public class IsEqual : IMemoryOperation
    {
        public static IsEqual I => new IsEqual();
        public int Inputs => 2;
        public int Outputs => 1;

        public void Exec(List<long> memory, long[] inputs, int[] outAddr)
        {
            memory[outAddr[0]] = inputs[0] == inputs[1] ? 1 : 0;
        }
    }
}