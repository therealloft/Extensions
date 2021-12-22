using System.Collections.Generic;

namespace Extensions
{
    public static class CollectionExtensions
    {
		/// <summary>
		/// Adds a IEnumerable to the collection
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <param name="otherEnumerable"></param>
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> otherEnumerable)
		{
			foreach (T enumerableElement in otherEnumerable)
			{
				collection.Add(enumerableElement);
			}
		}
	}
}
