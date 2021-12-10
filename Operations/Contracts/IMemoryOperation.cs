using System.Collections.Generic;

namespace AdventOfCode2019.Operations.Contracts
{
    public interface IMemoryOperation : IOperation
    {
        void Exec(List<long> memory, long[] inputs, int[] outputAddr);
    }
}