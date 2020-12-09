using IntcodeComputer.Operations.Contracts;

namespace IntcodeComputer.Operations
{
    public class ChangeRelativeIndex : IRelativeAdjustOperation
    {
        public static ChangeRelativeIndex I => new ChangeRelativeIndex();
        public int Inputs => 1;
        public int Outputs => 0;

        public int Exec(long[] inputs, int currentRelative)
        {
            return currentRelative + (int)inputs[0];
        }
    }
}