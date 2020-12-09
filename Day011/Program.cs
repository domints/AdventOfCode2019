using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using IntcodeComputer;

namespace Day011
{
    class Program
    {
        static Dictionary<(int x, int y), bool> hull = new Dictionary<(int x, int y), bool>();
        static ConcurrentQueue<long> inputBuffer = new ConcurrentQueue<long>();
        static HashSet<(int x, int y)> paintedBlocks = new HashSet<(int x, int y)>();
        static (int x, int y) currentPosition = (0, 0);
        static Direction currentDirection = Direction.Up;
        static bool shouldPaint = true;
        static void Main(string[] args)
        {
            inputBuffer.Enqueue(0L);
            hull.Add(currentPosition, false);

            var part1Comp = new Computer("input.txt");
            part1Comp.Run(inputBuffer, ProcessOutput);

            Console.WriteLine($"Part 1: {paintedBlocks.Count}");

            inputBuffer = new ConcurrentQueue<long>();
            hull = new Dictionary<(int x, int y), bool>();
            paintedBlocks = new HashSet<(int x, int y)>();
            currentDirection = Direction.Up;
            currentPosition = (0, 0);
            shouldPaint = true;

            inputBuffer.Enqueue(1L);
            hull.Add(currentPosition, true);

            var part2Comp = new Computer("input.txt");
            part2Comp.Run(inputBuffer, ProcessOutput);

            Console.WriteLine("Part 2:");
            PrintOutput();
        }

        static void PrintOutput()
        {
            var transX = Math.Max(hull.Keys.Min(k => k.x) * -1, 0);
            var transY = Math.Max(hull.Keys.Min(k => k.y) * -1, 0);
            var sizeX = transX + hull.Keys.Max(k => k.x) + 1;
            var sizeY = transY + hull.Keys.Max(k => k.y) + 1;
            bool[][] bitmap = new bool[sizeX][];
            for(int x = 0; x < sizeX; x++) bitmap[x] = new bool[sizeY];
            foreach(var kvp in hull)
            {
                try {
                    bitmap[kvp.Key.x + transX][kvp.Key.y + transY] = kvp.Value;}
                catch
                {
                    Console.WriteLine($"{sizeX},{sizeY}:{transX},{transY}={kvp.Key.x},{kvp.Key.y}");
                    throw;
                }
            }

            for(var y = 0; y < sizeY; y++)
            {
                for(var x = 0; x < sizeX; x++)
                {
                    Console.Write(bitmap[x][y] ? '█' : ' ');
                }
                Console.WriteLine();
            }
        }

        static void ProcessOutput(long value)
        {
            if(shouldPaint)
            {
                hull[currentPosition] = value == 1;
                paintedBlocks.Add(currentPosition);
                shouldPaint = false;
            }
            else 
            {
                currentPosition = ProcessMove(value);
                if(!hull.ContainsKey(currentPosition))
                    hull.Add(currentPosition, false);

                inputBuffer.Enqueue(hull[currentPosition] ? 1 : 0);
                shouldPaint = true;
            }
        }

        static (int x, int y) ProcessMove(long rotation)
        {
            var dirI = Overflow((int)currentDirection + (rotation == 1 ? 1 : -1));
            currentDirection = (Direction)dirI;

            switch(currentDirection)
            {
                case Direction.Up:
                    return (currentPosition.x, currentPosition.y - 1);
                case Direction.Down:
                    return (currentPosition.x, currentPosition.y + 1);
                case Direction.Left:
                    return (currentPosition.x - 1, currentPosition.y);
                case Direction.Right:
                    return (currentPosition.x + 1, currentPosition.y);
                default:
                    return (0, 0);
            }
        }

        static int Overflow(int value, int min = 0, int max = 3)
        {
            if(value < min)
                return max - (min - value) + 1;
            if(value > max)
                return min + (value - max) - 1;
            
            return value;
        }
    }
}
