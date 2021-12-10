using System;
using System.Collections.Generic;
using AdventOfCode2019.Operations.Contracts;

namespace AdventOfCode2019.Operations
{
    public class Store : IMemoryOperation
    {
        public static Store I(Action<long> storeAction) => new Store(storeAction);
        private readonly Action<long> storeAction;

        private Store() { }
        private Store(Action<long> storeAction)
        {
            this.storeAction = storeAction;
        }

        public int Inputs => 1;
        public int Outputs => 0;

        public void Exec(List<long> memory, long[] inputs, int[] outAddr)
        {
            storeAction(inputs[0]);
        }
    }
}