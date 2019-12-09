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

            int[] result = new int[LayerSize];
            for(int i = 0; i < LayerSize; i++)
            {
                int lix = 0;
                int colour = 2;
                while(colour == 2)
                {
                    colour = Image[lix++][i];
                }

                result[i] = colour;
            }

            var ixx = 0;
            for(int y = 0; y < Height; y++)
            {
                for(int x = 0; x < Width; x++)
                {
                    Console.Write(result[ixx++] == 1 ? '█' : ' ');
                }
                Console.WriteLine();
            }
        }
    }
}
