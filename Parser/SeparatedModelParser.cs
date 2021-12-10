using System;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2019.Parser
{
    public class SeparatedModelParser
    {
        private readonly string _separator;

        public SeparatedModelParser(string separator)
        {
            _separator = separator;
        }

        public T Parse<T>(string line)
            where T : ISeparatedModel, new()
        {
            var modelProperties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<IgnoreAttribute>() == null)
                .ToArray();
            if(modelProperties.Any(p => p.GetCustomAttribute<PositionAttribute>() == null))
            {
                throw new InvalidOperationException("All properties need position attribute.");
            }

            var result = new T();

            var data = line.Split(_separator);
            foreach(var p in modelProperties)
            {
                var position = p.GetCustomAttribute<PositionAttribute>().Position;
                var value = Convert.ChangeType(data[position].Trim(), p.PropertyType);
                p.SetValue(result, value);
            }

            return result;
        }
    }
}