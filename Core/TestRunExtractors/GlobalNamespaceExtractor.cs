using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QuickRunner.Core.TestRunExtractors
{
    public class GlobalNamespaceExtractor : TestRunExtractor
    {
        public GlobalNamespaceExtractor(RunnerOptions options)
            : base(options)
        {
            Namespaces = options.Namespaces;
        }

        public List<string> Namespaces { get; private set; }

        protected override IEnumerable<MethodInfo> FilterTests(IEnumerable<MethodInfo> tests)
        {
            return tests.Where(t => Namespaces.Any(ns => t.ReflectedType.Namespace.Contains(ns)));
        }
    }
}