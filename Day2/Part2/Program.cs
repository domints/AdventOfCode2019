using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static List<int> Input;
        static void Main(string[] args)
        {
            Input = File.ReadAllText("input.txt")
                .Split(',')
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => int.Parse(l))
                .ToList();
            for (int x = 0; x < 100; x++)
                for (int y = 0; y < 100; y++)
                {
                    Input[1] = x;
                    Input[2] = y;
                    var comp = new Computer(Input);
                    comp.Run();
                    if (comp.Memory[0] == 19690720)
                    {
                        Console.WriteLine((100 * x) + y);
                        return;
                    }
                }
        }
    }
}
