using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Extensions
{
    public static class TypeExtensions
    {
		public static IEnumerable<Type> GetAllConcreteTypes(this Type baseType, params Assembly[] assemblies)
		{
			if (assemblies.IsNullOrEmpty())
			{
				assemblies = AppDomain.CurrentDomain.GetAssemblies();
			}

			return assemblies
				.SelectMany(s => s.GetTypes())
				.Where(
					currentType =>
						baseType.IsAssignableFrom(currentType) &&
						!currentType.IsInterface &&
						!currentType.IsAbstract);
		}

		public static object CreateInstance(this Type genericType, Type concreteType)
		{
			Type finalType = genericType.MakeGenericType(concreteType);
			object instance = Activator.CreateInstance(finalType);
			return instance;
		}
	}
}
