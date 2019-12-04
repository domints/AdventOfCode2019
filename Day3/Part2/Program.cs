using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] _)
        {
            var paths = File.ReadAllLines("input.txt")
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Split(','))
                .ToList();

            Dictionary<(int x, int y), GridEntry> entries = new Dictionary<(int x, int y), GridEntry>();
            ParsePath(paths[0], entries, second: false);
            ParsePath(paths[1], entries, second: true);
            var crossing = entries
                .Where(kvp => kvp.Value.FirstCame && kvp.Value.SecondCame)
                .Select(kvp => kvp.Value)
                .ToList();
            Console.WriteLine(crossing.Min(c => c.FirstDistance + c.SecondDistance));
        }

        static void ParsePath(string[] steps, Dictionary<(int x, int y), GridEntry> entries, bool second = false)
        {
            int x = 0, y = 0, distance = 0;
            foreach(var s in steps.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var (xA, yA) = GetDirection(s[0]);
                var count = int.Parse(s.Substring(1));
                for(int i = 0; i < count; i++)
                {
                    x += xA;
                    y += yA;
                    distance++;
                    var key = (x, y);
                    if(entries.ContainsKey(key))
                    {
                        if(second)
                        {
                            var e = entries[key];
                            if(e.SecondCame) {}
                                //distance = e.FirstDistance;
                            else
                            {
                                e.SecondCame = true;
                                e.SecondDistance = distance;
                            }
                        }
                        else
                        {
                            var e = entries[key];
                            if(e.FirstCame) {}
                                //distance = e.FirstDistance;
                            else
                            {
                                e.FirstCame = true;
                                e.FirstDistance = distance;
                            }
                        }
                    }
                    else
                    {
                        var gridEntry = new GridEntry
                        {
                            FirstCame = !second,
                            SecondCame = second,
                            X = x,
                            Y = y
                        };
                        if(second)
                            gridEntry.SecondDistance = distance;
                        else
                            gridEntry.FirstDistance = distance;

                        entries.Add(key, gridEntry);
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
    }

    class GridEntry {
        public int X { get; set; }
        public int Y { get; set; }
        public bool FirstCame { get; set; }
        public bool SecondCame { get; set; }
        public int FirstDistance { get; set; }
        public int SecondDistance { get; set; }
    }
}
