using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        static Dictionary<(int x, int y), TileType> display = new Dictionary<(int x, int y), TileType>();
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
            comp.Run(inputBuffer: null, 3, ProcessOutput);

            Console.WriteLine(display.Values.Count(v => v == TileType.Block));
        }

        static void ProcessOutput(List<long> data)
        {
            var key = ((int)data[0], (int)data[1]);
            if(display.ContainsKey(key))
            {
                display[key] = (TileType)data[2];
            }
            else
                display.Add(key, (TileType)data[2]);
        }
    }

    enum TileType
    {
        Empty,
        Wall,
        Block,
        Paddle,
        Ball
    }
}
