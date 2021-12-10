using System.Linq;

namespace AdventOfCode2019.Solutions
{
    public class Day4 : IntSolution
    {
        public override long IntSolveA(string inputFile)
        {
            var input = Tools.ReadSeparated(inputFile, "-").ToList();
            var min = int.Parse(input[0]);
            var max = int.Parse(input[1]);
            int correct = 0;
            for(int i = min; i < max; i++)
            {
                if(CheckCorrect(i.ToString())) correct++;
            }

            return correct;
        }

        public override long IntSolveB(string inputFile)
        {
            var input = Tools.ReadSeparated(inputFile, "-").ToList();
            var min = int.Parse(input[0]);
            var max = int.Parse(input[1]);
            int correct = 0;
            for(int i = min; i < max; i++)
            {
                if(CheckCorrectB(i.ToString())) correct++;
            }

            return correct;
        }

        private bool CheckCorrect(string input)
        {
            bool hasDouble = false;
            for(int i = 0; i < input.Length - 1; i++)
            {
                if(input[i] > input[i + 1]) return false;
                if(input[i] == input[i + 1]) hasDouble = true;
            }

            return hasDouble;
        }

        static bool CheckCorrectB(string input)
        {
            bool hasDouble = false;
            var lastDigit = 'a';
            var sameDigitCount = 0;
            for(int i = 0; i < input.Length; i++)
            {
                if(i < input.Length - 1 && input[i] > input[i + 1]) return false;
                if(input[i] == lastDigit) sameDigitCount++;
                else {
                    if(sameDigitCount == 1) hasDouble = true;

                    sameDigitCount = 0;
                }

                lastDigit = input[i];
            }
            if(sameDigitCount == 1) hasDouble = true;

            return hasDouble;
        }
    }
}