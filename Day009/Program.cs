using System;
using System.Collections.Concurrent;
using IntcodeComputer;

namespace Day009
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            var comp = new Computer("input.txt");
            Console.Write("Part 1: ");
            comp.Run(inputBuffer: new ConcurrentQueue<long>(new [] { 1L }), outputMethod: (v) => Console.Write($"{v}"));
            Console.WriteLine();
            Console.Write("Part 2: ");
            comp = new Computer("input.txt");
            comp.Run(inputBuffer: new ConcurrentQueue<long>(new [] { 2L }), outputMethod: (v) => Console.Write($"{v}"));
            Console.WriteLine();
        }
    }
}
