using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Models;

namespace AdventOfCode2019
{
    public static class Extensions
    {
        public static IEnumerable<Type> GetImplementingTypes(this Type itype)
            => AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                .Where(t => t.GetInterfaces().Contains(itype));

        public static IEnumerable<LinkedListNode<T>> EnumerateNodes<T>(this LinkedList<T> source)
        {
            for (var node = source.First; node != null; node = node.Next)
                yield return node;
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
            => source.Select((item, index) => (item, index));

        public static string GetString(this IEnumerable<Coordinate> source, bool inline = false)
            => string.Join(inline ? ", " : "\r\n", source.Select(c => c.ToString()));

        public static T FindByKey<TKey, T>(this Dictionary<TKey, T> source, Func<TKey, bool> predicate)
        {
            var keyList = source.Keys.Where(predicate).ToList();
            if(keyList.Count != 1)
                return default;

            return source[keyList[0]];
        }
    }
}