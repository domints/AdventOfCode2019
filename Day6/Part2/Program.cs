using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        const string FROM = "YOU";
        const string TO = "SAN";
        static void Main(string[] _)
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

            Dictionary<string, int> santaPath = new Dictionary<string, int>();
            int orbitCount = -1;
            var m = objects[TO];
            var lastOrbit = m.CenterOfGravity;
            while (lastOrbit != null)
            {
                orbitCount++;
                santaPath.Add(lastOrbit.Id, orbitCount);
                lastOrbit = lastOrbit.CenterOfGravity;
            }

            var pathSum = -1;

            orbitCount = -1;
            m = objects[FROM];
            lastOrbit = m.CenterOfGravity;
            while (lastOrbit != null)
            {
                orbitCount++;
                if(santaPath.TryGetValue(lastOrbit.Id, out int length))
                {
                    pathSum = orbitCount + length;
                    break;
                }

                lastOrbit = lastOrbit.CenterOfGravity;
            }

            Console.WriteLine(pathSum);
        }

        static void LoadObjects(List<string> input, Dictionary<string, Mass> objects)
        {
            foreach (var i in input)
            {
                var data = i.Split(')');
                var mass = data[1].Trim();
                var center = data[0].Trim();
                objects.TryAdd(center, new Mass { Id = center });

                if (objects.TryGetValue(mass, out Mass m))
                {
                    m.CenterOfGravity = objects[center];
                }
                else
                {
                    objects.Add(mass, new Mass
                    {
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
