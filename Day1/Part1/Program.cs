using System;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var masses = File.ReadAllLines("input.txt")
                .Select(l => l.Trim())
                .Select(l => decimal.Parse(l));
            var fuel = masses.Sum(m => RequiredFuel(m));
            Console.WriteLine(fuel);
        }

        static decimal RequiredFuel(decimal moduleMass)
        {
            return Math.Floor(moduleMass / 3) - 2m;
        }
    }
}
