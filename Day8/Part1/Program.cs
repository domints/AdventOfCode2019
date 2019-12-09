using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        const int Width = 25;
        const int Height = 6;
        const int LayerSize = Width * Height;

        static List<List<int>> Image;

        static void Main(string[] args)
        {
            Image = new List<List<int>>();
            var image = File.ReadAllText("input.txt").Trim()
                .Select(l => int.Parse(l.ToString()))
                .ToList();

            int ix = 0;
            while(ix < image.Count)
            {
                var layer = new List<int>();
                Image.Add(layer);
                for(int x = 0; x < Width; x++)
                    for(int y = 0; y < Height; y++)
                    {
                        layer.Add(image[ix++]);
                    }
            }

            var correctLayer = Image.OrderBy(l => l.Count(i => i == 0)).First();

            var result = correctLayer.Count(i => i == 1) * correctLayer.Count(i => i == 2);
            Console.WriteLine(result);
        }
    }
}
