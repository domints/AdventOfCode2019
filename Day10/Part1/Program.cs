﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static bool[][] asteroids;
        static Dictionary<(int x, int y), List<(int x, int y)>> matches = new Dictionary<(int x, int y), List<(int x, int y)>>();
        static int xSize = 0;
        static int ySize = 0;
        static void Main(string[] _)
        {
            var map = File.ReadAllLines("input.txt")
                .Select(l => l.Trim().ToArray())
                .ToList();
            
            ySize = map.Count;
            xSize = map[0].Length;

            asteroids = new bool[xSize][];
            for(int x = 0; x < xSize; x++) asteroids[x] = new bool[ySize];

            for(int x = 0; x < xSize; x++)
                for(int y = 0; y < ySize; y++)
                {
                    asteroids[x][y] = map[y][x] == '#';
                }

            (int x, int y, int count) bestAsteroid = (-1, -1, -1);
            for(int x = 0; x < xSize; x++)
                for(int y = 0; y < ySize; y++)
                {
                    if(!asteroids[x][y]) continue;
                    var result = CheckAsteroids(x, y);
                    if(result > bestAsteroid.count)
                        bestAsteroid = (x, y, result);
                }

            PrintAsteroidMap(bestAsteroid.x, bestAsteroid.y);

            Console.WriteLine($"{bestAsteroid.x},{bestAsteroid.y}: {bestAsteroid.count}");
        }

        static void PrintAsteroidMap(int cx, int cy)
        {
            var ms = matches[(cx, cy)];
            for(int y = 0; y < ySize; y++)
            {
                for(int x = 0; x < xSize; x++)
                {
                    if(asteroids[x][y])
                    {
                        if(x == cx && y == cy)
                        {
                            Console.Write(" O");
                        }
                        else if(ms.Contains((x, y)))
                        {
                            Console.Write(" $");
                        }
                        else Console.Write(" #");
                    }
                    else Console.Write(" '");
                }
                Console.WriteLine();
            }
        }

        static int CheckAsteroids(int cx, int cy)
        {
            var count = 0;
            for(int x = 0; x < xSize; x++)
                for(int y = 0; y < ySize; y++)
                {
                    if(x == cx && y == cy) continue;
                    if(asteroids[x][y] && !AnyBetween(cx, cy, x, y)) 
                    {
                        if(!matches.ContainsKey((cx, cy)))
                        {
                            matches.Add((cx, cy), new List<(int x, int y)> { (x, y) });
                        }
                        else 
                        {
                            matches[(cx, cy)].Add((x, y));
                        }

                        count++;
                    }
                }

            return count;
        }

        static bool AnyBetween(int cx, int cy, int x, int y)
        {
            int xTrans = 1;
            int yTrans = 1;
            if(x < cx) xTrans = -1;
            if(y < cy) yTrans = -1;
            var xD = Math.Abs(x - cx);
            var yD = Math.Abs(y - cy);
            if(xD == 0)
            {
                for(int yy = 1; yy < yD; yy++)
                {
                    if(asteroids[cx][cy + (yy * yTrans)]) return true;
                }
            }
            else if(yD == 0)
            {
                for(int xx = 1; xx < xD; xx++)
                {
                    if(asteroids[cx + (xx * xTrans)][cy]) return true;
                }
            }
            else 
            {
                for(int yy = 1; yy < yD; yy++)
                    for(int xx = 1; xx < xD; xx++)
                    {
                        try {
                            if(((decimal)yD / yy) == ((decimal)xD / xx) && asteroids[cx + (xx * xTrans)][cy + (yy * yTrans)]) return true;
                        }
                        catch 
                        {
                            Console.WriteLine($"{cx},{cy} : {x}, {y} * {xTrans},{yTrans} = {xD},{yD} - {xx},{yy}");
                            throw;
                        }
                    }
            }

            return false;
        }
    }
}
