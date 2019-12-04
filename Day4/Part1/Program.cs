using System;

namespace Day4
{
    class Program
    {
        const int min = 248345;
        const int max = 746315;

        static void Main(string[] _)
        {
            int correct = 0;
            for(int i = min; i < max; i++)
            {
                if(CheckCorrect(i.ToString())) correct++;
            }
            Console.WriteLine(correct);
        }

        static bool CheckCorrect(string input)
        {
            bool hasDouble = false;
            for(int i = 0; i < input.Length - 1; i++)
            {
                if(input[i] > input[i + 1]) return false;
                if(input[i] == input[i + 1]) hasDouble = true;
            }

            return hasDouble;
        }
    }
}
