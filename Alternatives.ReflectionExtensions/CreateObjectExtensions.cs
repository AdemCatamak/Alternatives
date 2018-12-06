using System;
using System.Reflection;

namespace Alternatives.ReflectionExtensions
{
    public static class CreateObjectExtensions
    {
        public static object CreateInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static object CreateInstance(string strFullyQualifiedName)
        {
            object result = null;
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
            {
                result = Activator.CreateInstance(type);
            }

            if (result == null)
            {
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = asm.GetType(strFullyQualifiedName);
                    if (type != null)
                    {
                        result = Activator.CreateInstance(type);
                    }
                }
            }

            return result;
        }
    }
}