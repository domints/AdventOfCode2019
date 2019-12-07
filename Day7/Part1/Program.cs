using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        static void Main(string[] _)
        {
            Console.WriteLine("Start");
            var input = File.ReadAllText("input.txt")
                .Split(',')
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => int.Parse(l))
                .ToList();
            
            Console.WriteLine("Loaded input");
            var maxOut = 0;
            (int a, int b, int c, int d, int e) bs = (0,0,0,0,0);
            for(int a = 0; a < 5; a++)
                for(int b = 0; b < 5; b++)
                    for(int c = 0; c < 5; c++)
                        for(int d = 0; d < 5; d++)
                            for(int e = 0; e < 5; e++)
                            {
                                if(!Validate(a, b, c, d, e)) continue;
                                var result = RunComputers(a, b, c, d, e, input);
                                if(result > maxOut)
                                {
                                    maxOut = result;
                                    bs = (a, b, c, d, e);
                                }
                            }
            
            Console.WriteLine(maxOut);
            Console.WriteLine($"{bs.a},{bs.b},{bs.c},{bs.d},{bs.e}");
        }

        static bool Validate(int a, int b, int c, int d, int e)
        {
            return (new [] {a, b, c, d, e}).Distinct().Count() == 5;
        }

        static int RunComputers(int a, int b, int c, int d, int e, List<int> program)
        {
            var result = 0;
            var c1 = new Computer(program);
            var c2 = new Computer(program);
            var c3 = new Computer(program);
            var c4 = new Computer(program);
            var c5 = new Computer(program);
            var c1in = new Queue<int>(new [] { a, 0 });
            var c2in = new Queue<int>(new [] { b });
            var c3in = new Queue<int>(new [] { c });
            var c4in = new Queue<int>(new [] { d });
            var c5in = new Queue<int>(new [] { e });
            c1.Run(c1in, (v) => c2in.Enqueue(v));
            c2.Run(c2in, (v) => c3in.Enqueue(v));
            c3.Run(c3in, (v) => c4in.Enqueue(v));
            c4.Run(c4in, (v) => c5in.Enqueue(v));
            c5.Run(c5in, (v) => result = v);
            return c5.OutputBuffer.Last();
        }
    }
}
