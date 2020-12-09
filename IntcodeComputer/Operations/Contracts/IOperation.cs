namespace IntcodeComputer.Operations.Contracts
{
    public interface IOperation
    {
        int Inputs { get; }
        int Outputs { get; }
    }
}