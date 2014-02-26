using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace QuickRunner.Core.TestRunExtractors
{
    public static class FilterBy
    {
        public static IEnumerable<MethodInfo> Category(IEnumerable<MethodInfo> tests, IEnumerable<string> categories)
        {
            return tests.Where(test => MemberHasCategory(test, categories) || MemberHasCategory(test.ReflectedType, categories));
        }

        public static IEnumerable<MethodInfo> Namespace(IEnumerable<MethodInfo> tests, IEnumerable<string> namespaces)
        {
            return tests.Where(t => namespaces.Any(ns => t.ReflectedType.Namespace.Contains(ns)));
        }

        public static bool MemberHasCategory(MemberInfo info, IEnumerable<string> categories)
        {
            return info.GetCustomAttributes<CategoryAttribute>().Any(c => categories.Contains(c.Name));
        }
    }
}