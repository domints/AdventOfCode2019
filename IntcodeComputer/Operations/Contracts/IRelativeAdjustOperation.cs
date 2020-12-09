namespace IntcodeComputer.Operations.Contracts
{
    public interface IRelativeAdjustOperation : IOperation
    {
        int Exec(long[] inputs, int currentRelative);
    }
}