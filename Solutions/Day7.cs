using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019.Solutions
{
    public class Day7 : IntSolution
    {
        public override long IntSolveA(string inputFile)
        {
            var program = Tools.ReadSeparatedInt64s(inputFile);
            var maxOut = 0L;
            for (int a = 0; a < 5; a++)
                for (int b = 0; b < 5; b++)
                    for (int c = 0; c < 5; c++)
                        for (int d = 0; d < 5; d++)
                            for (int e = 0; e < 5; e++)
                            {
                                if (!Validate(a, b, c, d, e)) continue;
                                var result = RunComputers(a, b, c, d, e, program);
                                if (result > maxOut)
                                {
                                    maxOut = result;
                                }
                            }

            return maxOut;
        }

        public override long IntSolveB(string inputFile)
        {
            var program = Tools.ReadSeparatedInt64s(inputFile);
            var maxOut = 0L;
            for (int a = 5; a < 10; a++)
                for (int b = 5; b < 10; b++)
                    for (int c = 5; c < 10; c++)
                        for (int d = 5; d < 10; d++)
                            for (int e = 5; e < 10; e++)
                            {
                                if (!Validate(a, b, c, d, e)) continue;
                                var result = RunComputers(a, b, c, d, e, program, variantB: true);
                                if (result > maxOut)
                                {
                                    maxOut = result;
                                }
                            }

            return maxOut;
        }

        static bool Validate(long a, long b, long c, long d, long e)
        {
            return (new[] { a, b, c, d, e }).Distinct().Count() == 5;
        }

        private long RunComputers(long a, long b, long c, long d, long e, IEnumerable<long> program, bool variantB = false)
        {
            var result = 0L;
            var c1 = new Computer(program);
            var c2 = new Computer(program);
            var c3 = new Computer(program);
            var c4 = new Computer(program);
            var c5 = new Computer(program);
            var c1in = new ConcurrentQueue<long>(new[] { a, 0L });
            var c2in = new ConcurrentQueue<long>(new[] { b });
            var c3in = new ConcurrentQueue<long>(new[] { c });
            var c4in = new ConcurrentQueue<long>(new[] { d });
            var c5in = new ConcurrentQueue<long>(new[] { e });
            if (variantB)
            {
                var tasks = new List<Task>
                {
                    Task.Run(() => c1.Run(c1in, (v) => c2in.Enqueue(v))),
                    Task.Run(() => c2.Run(c2in, (v) => c3in.Enqueue(v))),
                    Task.Run(() => c3.Run(c3in, (v) => c4in.Enqueue(v))),
                    Task.Run(() => c4.Run(c4in, (v) => c5in.Enqueue(v))),
                    Task.Run(() => c5.Run(c5in, (v) => c1in.Enqueue(v)))
                };
                Task.WaitAll(tasks.ToArray());
            }
            else
            {
                c1.Run(c1in, (v) => c2in.Enqueue(v));
                c2.Run(c2in, (v) => c3in.Enqueue(v));
                c3.Run(c3in, (v) => c4in.Enqueue(v));
                c4.Run(c4in, (v) => c5in.Enqueue(v));
                c5.Run(c5in, (v) => result = v);
            }
            return c5.OutputBuffer.Last();
        }
    }
}