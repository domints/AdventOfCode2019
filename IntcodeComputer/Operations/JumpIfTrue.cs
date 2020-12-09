using System.Collections.Generic;
using IntcodeComputer.Operations.Contracts;

namespace IntcodeComputer.Operations
{
    public class JumpIfTrue : IJumpOperation
    {
        public static JumpIfTrue I => new JumpIfTrue();
        public int Inputs => 2;
        public int Outputs => 0;

        public int Exec(List<long> memory, long[] inputs)
        {
            return inputs[0] == 0 ? -1 : (int)inputs[1];
        }
    }
}