using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChiciStudios.ProjectPhoenix.Utils
{
    public static class ReflectionHelpers
    {
        public static IEnumerable<Type> GetImplementingTypes<T>()
        {
            var type = typeof(T);
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(c =>
                    c != type &&
                    c.IsClass &&
                    type.IsAssignableFrom(c));
        }

        public static T[] InstantiateAllImplementingTypes<T>()
        {
            return GetImplementingTypes<T>().Select(t => (T)Activator.CreateInstance(t)).ToArray();
        }
    }
}