using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using QuickRunner.Core.Extractors;

namespace QuickRunner.Core.TestRunExtractors
{
    public class NamespaceSplitExtractor : TestRunExtractor
    {
        public NamespaceSplitExtractor(IEnumerable<TestEnvironment> environments, string assemblyFilename, string assemblyPath, string configFilepath)
            : base(environments, assemblyFilename, assemblyPath, configFilepath)
        {
        }

        protected override IEnumerable<TestRun> GetRuns()
        {
            var namespaceGroups =
                from type in Assembly.GetTypes()
                let attrs = type.GetCustomAttributes(typeof(TestFixtureAttribute), true)
                where attrs != null && attrs.Length > 0
                group type by type.Namespace into nsGroups
                select nsGroups;

            var numEnvironments = Environments.Count;
            var runs = new TestRun[numEnvironments];
            for (var i = 0; i < namespaceGroups.Count(); i++)
            {
                var envIdx = i % numEnvironments;
                var run = runs[envIdx];
                var currentGroup = namespaceGroups.ElementAt(i);

                var tests = currentGroup.SelectMany(
                    x => x.GetMethods().Where(
                        m => m.GetCustomAttributes(typeof(TestAttribute), true).Length > 0
                    )).ToList();

                if (run == null)
                {
                    runs[envIdx] = new TestRun(Environments[envIdx], tests);
                }
                else
                {
                    run.Tests.AddRange(tests);
                }
            }

            return runs;
        }
    }
}