using System.Collections.Generic;
using IntcodeComputer.Operations.Contracts;

namespace IntcodeComputer.Operations
{
    public class JumpIfFalse : IJumpOperation
    {
        public static JumpIfFalse I => new JumpIfFalse();
        public int Inputs => 2;
        public int Outputs => 0;

        public int Exec(List<long> memory, long[] inputs)
        {
            return inputs[0] == 0 ? (int)inputs[1] : -1;
        }
    }
}