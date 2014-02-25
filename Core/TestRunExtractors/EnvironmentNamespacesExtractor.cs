using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace QuickRunner.Core.TestRunExtractors
{
    public class EnvironmentNamespacesExtractor : TestRunExtractor
    {
        public EnvironmentNamespacesExtractor(RunnerOptions options) 
            : base(options)
        {
        }

        protected override IEnumerable<MethodInfo> FilterTests(IEnumerable<MethodInfo> tests)
        {
            var namespaces = Environments.Where(env => env.Namespaces != null).SelectMany(env => env.Namespaces);
            return tests.Where(test => namespaces.Any(ns => test.DeclaringType.Namespace.Contains(ns)));
        }

        protected override IEnumerable<TestRun> CreateRuns(IEnumerable<MethodInfo> tests)
        {
            var namespaceGroups = tests.GroupBy(t => t.ReflectedType.Namespace);
            return
                from environment in Environments.Where(ns => ns.Namespaces != null)
                let envTests =
                    namespaceGroups.Where(nsg => environment.Namespaces.Contains(nsg.Key)).SelectMany(nsg => nsg)
                select new TestRun(environment, envTests.ToList());
        }
    }
}