using System;

namespace Assets.Networking.Utilities
{
    public static class TypeNamer
    {
        public static string GetTypeName(Type t)
        {
            if (t.FullName != null)
            {
                return t.FullName;
            }

            return $"{t.AssemblyQualifiedName}#{t.Name}";
        }
    }
}
