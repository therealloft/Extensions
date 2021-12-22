using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class EnumerableExtensions
	{
		private static readonly Random Random = new Random();
		public static T RandomElement<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.RandomElementUsing(new Random());
		}

		public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
		{
			int index = rand.Next(0, enumerable.Count());
			return enumerable.ElementAt(index);
		}

		public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable != null && enumerable.Any();
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable == null || !enumerable.Any();
		}

		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (T element in enumerable)
			{
				action?.Invoke(element);
			}
		}

		public static bool IsValid<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return false;
			}

			T[] array = enumerable.ToArray();

			if (array.Length == 0)
			{
				return false;
			}

			bool isValid = array.All(element => element != null);
			return isValid;
		}

		public static T[] AsArray<T>(this IEnumerable<T> enumerable)
		{
			return enumerable as T[] ?? enumerable.ToArray();
		}

		public static List<T> AsList<T>(this IEnumerable<T> enumerable)
		{
			return enumerable as List<T> ?? enumerable.ToList();
		}

		public static int RandomElementCount<T>(this IReadOnlyList<T> readOnlyList, int min = 0)
		{
			Random random = new Random();
			return Convert.ToInt32(random.NextDouble() * (readOnlyList.Count - min) + min);
		}

		public static IEnumerable<T> RandomUniqueElementsWithCount<T>(this IEnumerable<T> enumerable, int elementCount)
		{
			return enumerable.OrderBy(arg => Guid.NewGuid()).Take(elementCount);
		}

		public static IEnumerable<T> RandomUniqueElements<T>(this IEnumerable<T> enumerable, int min = 0)
		{
			T[] array = enumerable.AsArray();
			int randomElementCount = array.RandomElementCount(min);
			IEnumerable<T> result = array.RandomUniqueElementsWithCount(randomElementCount);

			return result;
		}
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sequence)
		{
			if (sequence == null)
			{
				throw new ArgumentNullException(nameof(sequence), "source or predicate is null.");
			}
			var result = sequence.ToArray();
			int dataLength = result.Length;
			if (dataLength == 0)
			{
				throw new InvalidOperationException("Sequence contains no elements.");
			}
			for (int index = 0; index < dataLength; index++)
			{
				int randomPoint = index + (int)(Random.NextDouble() * (dataLength - index));
				var tempData = result[randomPoint];
				result[randomPoint] = result[index];
				result[index] = tempData;
			}
			return result;
		}
	}
}