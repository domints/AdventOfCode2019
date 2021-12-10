using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Solutions
{
    public class Day3 : IntSolution
    {
        public override long IntSolveA(string inputFile)
        {
            var paths = Tools.ReadLines(inputFile)
                .Select(l => l.Split(','))
                .ToList();

            Dictionary<string, GridEntry> entries = new Dictionary<string, GridEntry>();
            ParsePath(paths[0], entries, second: false);
            ParsePath(paths[1], entries, second: true);
            var crossing = entries
                .Where(kvp => kvp.Value.FirstCame && kvp.Value.SecondCame)
                .Select(kvp => kvp.Value)
                .ToList();

            return crossing.Min(c => Math.Abs(c.X) + Math.Abs(c.Y));
        }

        public override long IntSolveB(string inputFile)
        {
            var paths = Tools.ReadLines(inputFile)
                .Select(l => l.Split(','))
                .ToList();

            Dictionary<(int x, int y), GridEntry> entries = new Dictionary<(int x, int y), GridEntry>();
            ParsePathB(paths[0], entries, second: false);
            ParsePathB(paths[1], entries, second: true);
            var crossing = entries
                .Where(kvp => kvp.Value.FirstCame && kvp.Value.SecondCame)
                .Select(kvp => kvp.Value)
                .ToList();

            return crossing.Min(c => c.FirstDistance + c.SecondDistance);
        }

        private void ParsePath(string[] steps, Dictionary<string, GridEntry> entries, bool second = false)
        {
            int x = 0, y = 0;
            foreach(var s in steps.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var (xA, yA) = GetDirection(s[0]);
                var count = int.Parse(s.Substring(1));
                for(int i = 0; i < count; i++)
                {
                    x += xA;
                    y += yA;
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

        private void ParsePathB(string[] steps, Dictionary<(int x, int y), GridEntry> entries, bool second = false)
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

        private (int xA, int yA) GetDirection(char sign)
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

        private string GetKey(int x, int y)
        {
            return $"{x}$${y}";
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