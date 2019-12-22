using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
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
            Console.WriteLine("Start");
            var input = File.ReadAllText("input.txt")
                .Split(',')
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => long.Parse(l))
                .ToList();
            
            Console.WriteLine("Loaded input");
            inputBuffer.Enqueue(0L);
            hull.Add(currentPosition, false);
            var comp = new Computer(input);
            comp.Run(inputBuffer, ProcessOutput);

            Console.WriteLine(paintedBlocks.Count);
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

        enum Direction {
            Up,
            Right,
            Down,
            Left
        }
    }
}
