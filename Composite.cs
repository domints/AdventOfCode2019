namespace AdventOfCode2019
{
    /// <summary>
    /// Don't look at me like that, it's nice to have some JS-like features in C# :D
    /// </summary>
    /// <typeparam name="T1">First composite type</typeparam>
    /// <typeparam name="T2">Second composite type</typeparam>
    public class Composite<T1, T2>
    {
        public Composite(T1 value)
        {
            _1 = value;
        }

        public Composite(T2 value)
        {
            _2 = value;
        }

        private readonly T1 _1;
        private readonly T2 _2;

        public static implicit operator Composite<T1, T2>(T1 value) => new Composite<T1, T2>(value);
        public static implicit operator Composite<T1, T2>(T2 value) => new Composite<T1, T2>(value);

        public static implicit operator T1(Composite<T1, T2> composite) => composite._1;
        public static implicit operator T2(Composite<T1, T2> composite) => composite._2;
    }
}