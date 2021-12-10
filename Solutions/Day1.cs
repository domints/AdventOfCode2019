using System;
using System.Linq;

namespace AdventOfCode2019.Solutions
{
    public class Day1 : IntSolution
    {
        public override long IntSolveA(string inputFile)
        {
            var masses = Tools.ReadDecimalLines(inputFile);
            return (long)masses.Sum(m => RequiredFuel(m));
        }

        public override long IntSolveB(string inputFile)
        {
            var masses = Tools.ReadDecimalLines(inputFile);
            return (long)masses.Sum(m => RequiredFuelB(m));
        }

        private decimal RequiredFuel(decimal moduleMass)
        {
            return Math.Floor(moduleMass / 3) - 2m;
        }

        private decimal RequiredFuelB(decimal moduleMass)
        {
            static decimal calcFuel(decimal input) => Math.Floor(input / 3) - 2m;
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