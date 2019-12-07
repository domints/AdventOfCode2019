using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] _)
        {
            Console.ReadKey();
            var programMemory = File.ReadAllText("input.txt")
                .Split(',')
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => int.Parse(l))
                .ToList();

            var input = new Queue<int>();
            input.Enqueue(1);

            var computer = new Computer(programMemory);
            computer.Run(input, (val) => Console.WriteLine($"cnt:{computer.ProgramCounter}, {val}"));
        }
    }
}
