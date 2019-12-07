using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = File.ReadAllLines("input.txt")
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();
            
            Dictionary<string, Mass> objects = new Dictionary<string, Mass>
            {
                ["COM"] = new Mass { Id = "COM" }
            };

            LoadObjects(map, objects);

            int orbitCount = 0;
            foreach(var m in objects.Values)
            {
                var lastOrbit = m.CenterOfGravity;
                while(lastOrbit != null)
                {
                    orbitCount++;
                    lastOrbit = lastOrbit.CenterOfGravity;
                }
            }

            Console.WriteLine(orbitCount);
        }

        static void LoadObjects(List<string> input, Dictionary<string, Mass> objects)
        {
            foreach(var i in input)
            {
                var data = i.Split(')');
                var mass = data[1].Trim();
                var center = data[0].Trim();
                objects.TryAdd(center, new Mass { Id = center });

                if(objects.TryGetValue(mass, out Mass m))
                {
                    m.CenterOfGravity = objects[center];
                }
                else
                {
                    objects.Add(mass, new Mass {
                        Id = mass,
                        CenterOfGravity = objects[center]
                    });
                }
            }
        }
    }

    class Mass
    {
        public string Id { get; set; }
        public Mass CenterOfGravity { get; set; } 
    }
}
