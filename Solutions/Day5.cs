using System.Collections.Concurrent;

namespace AdventOfCode2019.Solutions
{
    public class Day5 : IntSolution
    {
        public override long IntSolveA(string inputFile)
        {
            var computer = new Computer(inputFile);
            var input = new ConcurrentQueue<long>(new [] { 1L });
            var output = 0L;
            computer.Run(input, (v) => output = v);
            return output;
        }

        public override long IntSolveB(string inputFile)
        {
            var computer = new Computer(inputFile);
            var input = new ConcurrentQueue<long>(new [] { 5L });
            var output = 0L;
            computer.Run(input, (v) => output = v);
            return output;
        }
    }
}