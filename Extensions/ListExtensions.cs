using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class ListExtensions
	{
		public static void Replace<T>(this IList<T> list, T current, T replacement)
		{
			int index = list.IndexOf(current);
			if (index >= 0)
			{
				list[index] = replacement;
			}
		}

		public static void RemoveRange<T>(this IList<T> list, IEnumerable<T> elementsToRemove)
		{
			foreach (T element in elementsToRemove.Where(list.Contains))
			{
				list.Remove(element);
			}
		}

		public static void SafeForEach<T>(this IList<T> list, Action<T> action, bool removeNullElements = true)
		{
			if (list.IsNullOrEmpty()) return;

			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == null)
				{
					if (removeNullElements)
					{
						list.RemoveAt(i);
					}

					continue;
				}

				action?.Invoke(list[i]);
			}
		}

		public static void AddIfNotContains<T>(this IList<T> list, T element)
		{
			if (list == null || list.Contains(element)) return;

			list.Add(element);
		}

		public static void SafeRemove<T>(this IList<T> list, T element)
		{
			if (list.IsNullOrEmpty()) return;

			if (list.Contains(element))
			{
				list.Remove(element);
			}
		}

		public static void Shuffle<T>(this IList<T> list)
		{
			Random random = new Random();

			int count = list.Count;
			int last = count - 1;
			for (int i = 0; i < last; ++i)
			{
				int r = Convert.ToInt32(random.NextDouble() * (count - i) + i);
				T tmp = list[i];
				list[i] = list[r];
				list[r] = tmp;
			}
		}

		public static void Shuffle<T>(this IList<T> list, Random rng)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int key = rng.Next(n + 1);
				T value = list[key];
				list[key] = list[n];
				list[n] = value;
			}
		}

		public static void AddRange<T>(this IList<T> list, IReadOnlyList<T> otherEnumerable, int count)
		{
			for (int i = 0; i < count; i++)
			{
				list.Add(otherEnumerable[i]);
			}
		}
	}
}