using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2019.Parser;

namespace AdventOfCode2019
{
    public static class Tools
    {
        public static IEnumerable<int> ReadIntegerLines(string path)
        {
            return ReadLines(path)
                .Select(l => int.Parse(l));
        }

        public static IEnumerable<long> ReadInt64Lines(string path)
        {
            return ReadLines(path)
                .Select(l => long.Parse(l));
        }

        public static IEnumerable<decimal> ReadDecimalLines(string path)
        {
            return ReadLines(path)
                .Select(l => decimal.Parse(l));
        }

        public static IEnumerable<string> ReadSeparated(string path, string separator = ",")
        {
            return File.ReadAllText(path).Trim().Split(separator);
        }

        public static IEnumerable<int> ReadSeparatedIntegers(string path)
        {
            return ReadSeparated(path).Select(n => int.Parse(n));
        }

        public static IEnumerable<long> ReadSeparatedInt64s(string path)
        {
            return ReadSeparated(path).Select(n => long.Parse(n));
        }

        public static string GetTestOutput(int day, bool partB)
        {
            return File.ReadAllLines($"./TestOutput/{day}.txt")[partB ? 1 : 0];
        }

        public static IEnumerable<T> ReadSeparatedModelLines<T>(string path, string separator = " ")
            where T : ISeparatedModel, new()
        {
            var parser = new SeparatedModelParser(separator);
            return ReadLines(path)
                .Select(l => parser.Parse<T>(l));
        }

        public static IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path).Where(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.Trim());
        }
    }
}