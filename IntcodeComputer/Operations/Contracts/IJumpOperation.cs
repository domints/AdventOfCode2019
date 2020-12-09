using System.Collections.Generic;

namespace IntcodeComputer.Operations.Contracts
{
    public interface IJumpOperation: IOperation
    {
        int Exec(List<long> memory, long[] inputs);
    }
}