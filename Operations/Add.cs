using System.Collections.Generic;
using AdventOfCode2019.Operations.Contracts;

namespace AdventOfCode2019.Operations
{
    public class Add : IMemoryOperation
    {
        public static Add I => new Add();

        public int Inputs => 2;
        public int Outputs => 1;

        public void Exec(List<long> memory, long[] inputs, int[] outAddr)
        {
            memory[outAddr[0]] = inputs[0] + inputs[1];
        }
    }
}