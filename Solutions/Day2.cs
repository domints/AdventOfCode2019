using System.Linq;

namespace AdventOfCode2019.Solutions
{
    public class Day2 : IntSolution
    {
        public override long IntSolveA(string inputFile)
        {
            var computer = new Computer(inputFile);
            computer.Memory[1] = 12;
            computer.Memory[2] = 2;
            computer.Run();

            return computer.Memory[0];
        }

        public override long IntSolveB(string inputFile)
        {
            var program = Tools.ReadSeparatedInt64s(inputFile);
            for (int x = 0; x <= 99; x++)
            {
                for (int y = 0; y <= 99; y++)
                {
                    var computer = new Computer(program);
                    computer.Memory[1] = x;
                    computer.Memory[2] = y;
                    computer.Run();
                    if (computer.Memory[0] == 19690720)
                        return 100 * x + y;
                }
            }
            return 0;
        }
    }
}