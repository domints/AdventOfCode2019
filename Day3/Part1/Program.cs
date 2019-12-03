using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var paths = File.ReadAllLines("input.txt")
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Split(','))
                .ToList();

            Dictionary<string, GridEntry> entries = new Dictionary<string, GridEntry>();
            ParsePath(paths[0], entries, second: false);
            ParsePath(paths[1], entries, second: true);
            var crossing = entries
                .Where(kvp => kvp.Value.FirstCame && kvp.Value.SecondCame)
                .Select(kvp => kvp.Value)
                .ToList();
            Console.WriteLine(crossing.Min(c => Math.Abs(c.X) + Math.Abs(c.Y)));
        }

        static void ParsePath(string[] steps, Dictionary<string, GridEntry> entries, bool second = false)
        {
            int x = 0, y = 0;
            foreach(var s in steps.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var dir = GetDirection(s[0]);
                var count = int.Parse(s.Substring(1));
                for(int i = 0; i < count; i++)
                {
                    x += dir.xA;
                    y += dir.yA;
                    var key = GetKey(x, y);
                    if(entries.ContainsKey(key))
                    {
                        if(second)
                            entries[key].SecondCame = true;
                        else
                            entries[key].FirstCame = true;
                    }
                    else
                    {
                        entries.Add(key, new GridEntry
                        {
                            FirstCame = !second,
                            SecondCame = second,
                            X = x,
                            Y = y
                        });
                    }
                }
            }
        }

        static (int xA, int yA) GetDirection(char sign)
        {
            return sign switch
            {
                'R' => (1, 0),
                'L' => (-1, 0),
                'U' => (0, -1),
                'D' => (0, 1),
                _ => (0, 0),
            };
        }

        static string GetKey(int x, int y)
        {
            return $"{x}$${y}";
        }
    }

    class GridEntry {
        public int X { get; set; }
        public int Y { get; set; }
        public bool FirstCame { get; set; }
        public bool SecondCame { get; set; }
    }
}
