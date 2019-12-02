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
            Func<decimal, decimal> calcFuel = (input) => Math.Floor(input / 3) - 2m;
            var addedFuel = calcFuel(moduleMass);
            var fuel = addedFuel;
            while(addedFuel > 0) {
                addedFuel = Math.Max(calcFuel(addedFuel), 0);
                fuel += addedFuel;
            }
            
            return fuel;
        }
    }
}
