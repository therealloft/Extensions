using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Extensions
{
    public static class DictionaryExtensions
    {
		/// <summary>
		/// Adds many keys with default value
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dictionary"></param>
		/// <param name="keys"></param>
		/// <param name="value"></param>
		public static void AddManyKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys, TValue value)
		{
			if (dictionary == null) return;

			foreach (TKey key in keys)
			{
				if (key == null || dictionary.ContainsKey(key)) continue;

				dictionary.Add(key, value);
			}
		}

		/// <summary>
		/// Returns value for key if exists or default{<typeparamref name="TValue"/>}
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dictionary"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
			TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		/// <summary>
		/// Returns value for key if exists or <paramref name="defaultValue"/>.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dictionary"></param>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
			TKey key, TValue defaultValue)
		{
			TValue value;
			if (dictionary.TryGetValue(key, out value))
			{
				return value;
			}
			return defaultValue;
		}
		/// <summary>
		/// Returns the <paramref name="dictionary"/> as read-only.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dictionary"></param>
		/// <returns></returns>
		public static IDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			return new ReadOnlyDictionary<TKey, TValue>(dictionary);
		}
	}
}
