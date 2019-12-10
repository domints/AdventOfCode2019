using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var input = File.ReadAllText("input.txt")
                .Split(',')
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => long.Parse(l))
                .ToList();
            
            Console.WriteLine("Loaded input");

            var comp = new Computer(input);
            comp.Run();
            Console.WriteLine(string.Join(',', comp.Memory));
        }
    }
}
