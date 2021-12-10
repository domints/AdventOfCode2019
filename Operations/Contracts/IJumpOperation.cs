using System.Collections.Generic;

namespace AdventOfCode2019.Operations.Contracts
{
    public interface IJumpOperation: IOperation
    {
        int Exec(List<long> memory, long[] inputs);
    }
}