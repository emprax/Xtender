using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Xtender.DependencyInjection
{
    /// <summary>
    /// Extensions based on the <see cref="IEnumerable{T}"/> interface in regards to the Xtender-library.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Creates a <see cref="ConcurrentDictionary{TKey,TValue}"/> from a collection <see>
        ///     <cref>IEnumerable{KeyValuePair{TKey,TValue}}</cref>
        /// </see>
        /// and ensures that the capacity is set to the count of the collection and the concurrency-level is set to the default value.
        /// </summary>
        /// <typeparam name="TKey">Type of the dictionary key.</typeparam>
        /// <typeparam name="TValue">Type of the dictionary value.</typeparam>
        /// <param name="enumerable">The to-be converted collection containing the keys and values for the concurrent-dictionary.</param>
        /// <returns>The resulting concurrent-dictionary.</returns>
        public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            var items = enumerable.ToList();
            var dictionary = new ConcurrentDictionary<TKey, TValue>(Environment.ProcessorCount, items.Count);
            foreach (var item in items)
            {
                ((IDictionary<TKey, TValue>)dictionary).Add(item.Key, item.Value);
            }

            return dictionary;
        }
    }
}