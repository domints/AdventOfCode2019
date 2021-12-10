using System;
using System.Linq;
using AdventOfCode2019.Solutions;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Please, pass at least the day!");
                return;
            }

            if (!int.TryParse(args[^1], out int day)) 
            {
                Console.WriteLine($"<{args[^1]}> is not a number. Exiting!");
                return;
            }

            var fileName = $"{day}.txt";
            var testMode = false;
            var partB = false;
            foreach (var arg in args)
            {
                if (arg.Contains('='))
                {
                    var data = arg.Split('=');
                    switch (data[0]) 
                    {
                        case "--file":
                        case "-f":
                            fileName = $"{day}_{data[1]}.txt";
                            break;

                        case "--part":
                        case "-p":
                            partB = data[1].ToLowerInvariant() == "b";
                            break;
                    }
                }
                else
                {
                    switch(arg)
                    {
                        case "-b":
                            partB = true;
                            break;

                        case "-t":
                            fileName = $"{day}_test.txt";
                            testMode = true;
                            break;
                    }
                }
            }
            
            var sol = GetSolutionForDay(day);
            var path = $"./Input/{fileName}";
            var output = string.Empty;
            if (partB)
                output = sol.SolveB(path);
            else
                output = sol.SolveA(path);

            if (testMode)
            {
                var expected = Tools.GetTestOutput(day, partB);
                if(expected == output)
                    Console.WriteLine("TEST OK");
                else
                    Console.WriteLine($"TEST FAILED.\r\nExpected: {expected}\r\nOutput: {output}");
            }
            else
            {
                Console.WriteLine($"OUTPUT: {output}");
            }
        }

        static ISolution GetSolutionForDay(int day)
        {
            var solutionType = typeof(ISolution)
                .GetImplementingTypes()
                .FirstOrDefault(t => t.Name == $"Day{day}");
            if(solutionType == null)
                throw new NotImplementedException($"Solution for day {day} doesn't exist!");

            return (ISolution)Activator.CreateInstance(solutionType);
        }
    }
}
