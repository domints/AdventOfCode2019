namespace AdventOfCode2019.Parser
{
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class PositionAttribute : System.Attribute
    {
        readonly int _position;
        
        public PositionAttribute(int position)
        {
            _position = position;
        }
        
        public int Position
        {
            get { return _position; }
        }
    }
}