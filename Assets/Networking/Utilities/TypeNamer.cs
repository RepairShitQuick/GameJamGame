using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Networking.Utilities
{
    public static class TypeNamer
    {
        private static Dictionary<string, Type> typeNamesByPath;

        static TypeNamer()
        {
            var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes());
            foreach (var type in allTypes)
            {
                typeNamesByPath.Add(GetTypeName(type), type);
            }
        }
        public static string GetTypeName(Type t)
        {
            if (t.FullName != null)
            {
                return t.FullName;
            }

            return $"{t.AssemblyQualifiedName}#{t.Name}";
        }

        public static Type GetType(string name)
        {
            return typeNamesByPath[name];
        }
    }
}
