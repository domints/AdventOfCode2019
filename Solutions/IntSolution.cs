namespace AdventOfCode2019.Solutions
{
    public abstract class IntSolution : ISolution
    {
        public abstract long IntSolveA(string inputFile);
        public abstract long IntSolveB(string inputFile);
        
        public string SolveA(string inputFile)
        {
            return IntSolveA(inputFile).ToString();
        }

        public string SolveB(string inputFile)
        {
            return IntSolveB(inputFile).ToString();
        }
    }
}