using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IntcodeComputer;

namespace Day013
{
    class Program
    {
        static Dictionary<(int x, int y), TileType> display = new Dictionary<(int x, int y), TileType>();

        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            var part1Comp = new Computer("input.txt");
            part1Comp.Run(inputBuffer: null, 3, ProcessOutput);

            Console.Write("Part 1: ");
            Console.WriteLine(display.Values.Count(v => v == TileType.Block));

            var part2Comp = new Computer("input.txt");
            part2Comp.Memory[0] = 2;
            
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
}
