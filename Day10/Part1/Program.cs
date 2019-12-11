using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static List<List<bool>> asteroids = new List<List<bool>>();
        static void Main(string[] _)
        {
            var map = File.ReadAllLines("input.txt")
                .Select(l => l.Trim().ToArray())
                .ToList();

            foreach(var line in map)
            {
                var l = new List<bool>();
                asteroids.Add(l);
                foreach(var ch in line)
                {
                    l.Add(ch == '#');
                }
            }

            for(int x = 0; x < asteroids.Count; x++)
            {
                for(int y = 0; y < asteroids[x].Count; y++)
                {
                    //Console.Write(asteroids[x][y] ? '＃': '＇');
                    Console.Write(asteroids[x][y] ? " #": " '");
                }
                Console.WriteLine();
            }
        }
    }
}
