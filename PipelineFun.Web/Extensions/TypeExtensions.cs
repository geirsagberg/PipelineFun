using System;
using System.Linq;
using System.Reflection;

namespace PipelineFun.Web.Extensions
{
    public static class TypeExtensions
    {
        public static bool Implements<TInterface>(this TypeInfo typeInfo) =>
            typeInfo?.ImplementedInterfaces.Contains(typeof(TInterface)) == true;

        public static bool Implements<TInterface>(this Type type) =>
            type?.GetInterfaces().Contains(typeof(TInterface)) == true;

        public static bool Implements(this TypeInfo typeInfo, Type type) =>
            typeInfo?.ImplementedInterfaces.Contains(type) == true;
    }
}