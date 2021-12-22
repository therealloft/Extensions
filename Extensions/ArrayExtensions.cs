using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class ArrayExtensions
    {
		/// <summary>
		/// Returns empty T array.
		/// </summary>
		public static T[] Empty<T>(this T[] sourceArray){
			return Array.Empty<T>();
        }
		/// <summary>
		/// Join multiple arrays and retuen a new array with elements from all arrays.
		/// </summary>
		/// <typeparam name="T">The type of the elements of array.</typeparam>
		/// <param name="sourceArray">The one-dimensional, zero-based System.Array to copy of.</param>
		/// <param name="arrays">Multiple of one-dimensional, zero-based System.Array to copy of.</param>
		/// <returns>A new array contains all elements from all arrays.</returns>
		public static T[] JoinArrays<T>(this T[] sourceArray, params T[][] arrays)
		{
			if (sourceArray == null || arrays == null)
			{
				throw new ArgumentNullException(nameof(sourceArray), "source or predicate is null.");
			}
			if (arrays == null)
			{
				throw new ArgumentNullException(nameof(arrays), "arrays is null.");
			}
			if (arrays.Any(i => i == null))
			{
				throw new ArgumentNullException(nameof(arrays), "arrays contains null.");
			}
			int sourceArrayLength = sourceArray.Length;
			var result = new T[sourceArrayLength + arrays.Sum(a => a.Length)];
			sourceArray.CopyTo(result, 0);
			int offset = sourceArrayLength;
			foreach (var array in arrays)
			{
				array.CopyTo(result, offset);
				offset += array.Length;
			}
			return result;
		}	

		/// <summary>
		/// Join multiple arrays and retuen a new array with elements from all arrays
		/// </summary>
		/// <typeparam name="T">The type of the elements of array.</typeparam>
		/// <param name="arrays">Multiple of one-dimensional, zero-based System.Array to copy of.</param>
		/// <returns>A new array contains all elements from all arrays.</returns>
		public static T[] JoinArrays<T>(this T[][] arrays)
		{
			if (arrays == null)
			{
				throw new ArgumentNullException(nameof(arrays), "source or predicate is null.");
			}
			return new T[0].JoinArrays(arrays);
		}

		/// <summary>
		/// Get the column on a jagged array
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="jaggedArray"></param>
		/// <param name="column"></param>
		/// <returns>the column of a jagged array</returns>
		public static IEnumerable<T> GetColumn<T>(this T[][] jaggedArray, int column)
        {
            return jaggedArray.Select(row => row[column]);
        }
        /// <summary>
        /// Shift an array element to the left
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="callback"></param>
        public static void ShiftLeft<T>(this T[] array, Action<T, T, int> callback = null)
        {
            T temp = array[0];
            int lastIndex = array.Length - 1;
            for (int i = 0; i < lastIndex; i++)
            {
                int j = i + 1;

                callback?.Invoke(array[j], array[i], i);

                array[i] = array[j];
            }

            callback?.Invoke(temp, array[lastIndex], lastIndex);
            array[lastIndex] = temp;
        }
		/// <summary>
		/// Shift an array element to the right
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		/// <param name="callback"></param>
		public static void ShiftRight<T>(this T[] array, Action<T, T, int> callback = null)
		{
			int lastIndex = array.Length - 1;
			T temp = array[lastIndex];

			for (int i = lastIndex; i > 0; i--)
			{
				int j = i - 1;

				callback?.Invoke(array[j], array[i], i);

				array[i] = array[j];
			}

			callback?.Invoke(temp, array[0], 0);
			array[0] = temp;
		}

		/// <summary>
		/// Shifts a column up in a jagged array
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array2d"></param>
		/// <param name="column"></param>
		/// <param name="callback"></param>
		public static void ShiftUp<T>(this T[][] array2d, int column, Action<T, T, int> callback = null)
		{
			T temp = array2d[0][column];

			int lastIndex = array2d.Length - 1;
			for (int i = 0; i < lastIndex; i++)
			{
				int j = i + 1;

				callback?.Invoke(array2d[j][column], array2d[i][column], i);

				array2d[i][column] = array2d[j][column];
			}

			callback?.Invoke(temp, array2d[lastIndex][column], lastIndex);
			array2d[lastIndex][column] = temp;
		}
		/// <summary>
		/// Shifts a column down in a jagged array
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array2d"></param>
		/// <param name="column"></param>
		/// <param name="callback"></param>
		public static void ShiftDown<T>(this T[][] array2d, int column, Action<T, T, int> callback = null)
		{
			int lastIndex = array2d.Length - 1;
			T temp = array2d[lastIndex][column];

			for (int i = array2d.Length - 1; i > 0; i--)
			{
				int j = i - 1;

				callback?.Invoke(array2d[j][column], array2d[i][column], i);

				array2d[i][column] = array2d[j][column];
			}

			callback?.Invoke(temp, array2d[0][column], 0);
			array2d[0][column] = temp;
		}


		public static int NearestValidIndex<T>(this IReadOnlyList<T> list, int index)
		{
			int count = list.Count;
			if (index < 0)
			{
				return 0;
			}

			if (index >= count)
			{
				return count - 1;
			}

			return index;
		}

		public static T GetRotatedElementWhenIndexInvalid<T>(this IReadOnlyList<T> list, int index)
		{
			return list[list.GetRotatedIndexWhenInvalid(index)];
		}

		public static int GetRotatedIndexWhenInvalid<T>(this IReadOnlyList<T> list, int index)
		{
			int count = list.Count;
			if (index < 0)
			{
				return count - 1;
			}

			if (index >= count)
			{
				return 0;
			}
			return index;
		}
	}
}
