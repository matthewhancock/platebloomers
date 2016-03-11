using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.Util {
    public static class Reflection {
        public static T[] GetImplementorsOfInterface<T>() {
            List<T> objects = new List<T>();
            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(q => typeof(T).IsAssignableFrom(q) && q.IsClass && !q.IsAbstract)) {
                objects.Add((T)Activator.CreateInstance(type, null));
            }
            return objects.ToArray();
        }
        public static T[] GetInheritorsOfClass<T>() {
            List<T> objects = new List<T>();
            var types = Assembly.GetAssembly(typeof(T)).GetTypes();
            foreach (Type type in types.Where(q => q.IsSubclassOf(typeof(T)) && q.IsClass && !q.IsAbstract)) {
                objects.Add((T)Activator.CreateInstance(type, null));
            }
            return objects.ToArray();
        }
        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(Type o) where T : Attribute {
            return o.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(T)));
        }
        public static T GetAttributeForProperty<T>(PropertyInfo Property) where T : Attribute {
            var a = Property.GetCustomAttributes(true);
            foreach (var i in a) {
                if (i is T) {
                    return (T)i;
                }
            }
            return null;
        }
    }
}
