using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static List<int> _pgm;
        static int _pcnt = 0;
        static void Main(string[] _)
        {
            _pgm = File.ReadAllText("input.txt")
                .Split(',')
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => int.Parse(l))
                .ToList();

            while(Run()) ;
            Console.WriteLine(_pgm[0]);
            File.WriteAllText("output.txt", string.Join(',', _pgm));
        }

        static bool Run()
        {
            switch(_pgm[_pcnt])
            {
                case 99:
                    return false;

                case 1:
                    Add();
                    break;

                case 2: 
                    Multiply();
                    break;
                
                default:
                    throw new InvalidOperationException("Unknown OPcode!");
            }

            return true;
        }

        static void Add()
        {
            _pgm[_pgm[_pcnt + 3]] = _pgm[_pgm[_pcnt + 1]] + _pgm[_pgm[_pcnt + 2]];
            _pcnt += 4;
        }

        static void Multiply()
        {
            _pgm[_pgm[_pcnt + 3]] = _pgm[_pgm[_pcnt + 1]] * _pgm[_pgm[_pcnt + 2]];
            _pcnt += 4;
        }
    }
}
