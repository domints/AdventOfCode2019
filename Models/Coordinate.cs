namespace AdventOfCode2019.Models
{
    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Value => X + Y;

        public static Coordinate FromString(string data)
        {
            var values = data.Split(',');
            return new Coordinate
            {
                X = int.Parse(values[0].Trim()),
                Y = int.Parse(values[1].Trim())
            };
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static implicit operator Coordinate((int x, int y) input) => new Coordinate { X = input.x, Y = input.y };
    }
}