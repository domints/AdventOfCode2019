using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static Dictionary<(int x, int y), TileType> display = new Dictionary<(int x, int y), TileType>();
        static ConcurrentQueue<long> inputBuffer = new ConcurrentQueue<long>();
        static long score = 0;
        static bool checkInputs = true;
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
            Console.Clear();
            Task.Run(CheckInput);
            comp.Run(inputBuffer: inputBuffer, 3, ProcessOutput);
            checkInputs = false;
            Console.WriteLine();
            Console.WriteLine(display.Values.Count(v => v == TileType.Block));
            Console.WriteLine($"Max: {display.Keys.Max(k => k.x)},{display.Keys.Max(k => k.y)}");
        }

        static void CheckInput()
        {
            while(checkInputs)
            {
                Thread.Sleep(200);
                var key = Console.ReadKey(true);
                switch(key.Key)
                {
                    case ConsoleKey.A:
                        inputBuffer.Enqueue(-1);
                        break;
                    case ConsoleKey.D:
                        inputBuffer.Enqueue(1);
                        break;
                    case ConsoleKey.Escape:
                        throw new Exception("LOL STOP THE APP!");
                    
                    default:
                        inputBuffer.Enqueue(0);
                        break;
                }
            }
        }

        static void ProcessOutput(List<long> data)
        {
            var key = (x: (int)data[0], y: (int)data[1]);
            if(key == (-1, 0)) 
            {
                score = data[2];
                Console.SetCursorPosition(42, 0);
                Console.Write($"Score: {score}");
                return;
            }

            if(display.ContainsKey(key))
            {
                display[key] = (TileType)data[2];
            }
            else
                display.Add(key, (TileType)data[2]);

            Console.SetCursorPosition(key.x, key.y);
            Console.Write(GetChar((TileType)data[2]));
        }

        static char GetChar(TileType type)
        {
            switch(type)
            {
                case TileType.Wall:
                    return (char)219;
                case TileType.Block:
                    return (char)254;
                case TileType.Paddle:
                    return '_';
                case TileType.Ball:
                    return 'o';

                default:
                    return ' ';
            }
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
