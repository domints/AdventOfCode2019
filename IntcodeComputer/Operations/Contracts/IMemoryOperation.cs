using System.Collections.Generic;

namespace IntcodeComputer.Operations.Contracts
{
    public interface IMemoryOperation : IOperation
    {
        void Exec(List<long> memory, long[] inputs, int[] outputAddr);
    }
}