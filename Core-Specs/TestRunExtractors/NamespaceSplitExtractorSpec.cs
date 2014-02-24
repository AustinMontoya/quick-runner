using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using QuickRunner.Core;
using QuickRunner.Core.TestRunExtractors;

namespace Core_Specs.TestRunExtractors
{
    [TestFixture]
    public class NamespaceSplitExtractorSpec
    {
        private List<TestEnvironment> _environments;

        private const string AssemblyPath = "../../lib/test-assembly/";
        private const string AssemblyFilename = "Slowlenium.dll";

        [SetUp]
        public void BeforeEach()
        {   
            _environments = new List<TestEnvironment>
            {
                new TestEnvironment {Name = "foo"},
                new TestEnvironment {Name = "bar"},
                new TestEnvironment {Name = "baz"},
                new TestEnvironment {Name = "foz"}
            };
        }

        [Test]
        public void NumberOfRunsShouldEqualNumberOfEnvironments()
        {
            Assert.AreEqual(GetTestRuns().Count(), _environments.Count);
        }

        [Test]
        public void EachRunShouldOnlyHaveTestsWithinExpectedNamespaces()
        {
            var runs = GetTestRuns();
            // This only tests when number of namespaces equals number of environments
            foreach (var testRun in runs)
            {
                var ns = testRun.Tests.First().ReflectedType.Namespace;
                Assert.IsTrue(testRun.Tests.All(t => t.ReflectedType.Namespace == ns));
            }
        }

        [Test]
        public void RunsShouldContainMultipleNamespacesWhenThereAreMoreNamespacesThanEnvironments()
        {
            var runs =
                new NamespaceSplitExtractor(_environments.Take(2), AssemblyFilename, AssemblyPath, null).Execute();

            Assert.AreEqual(2, runs.ElementAt(0).Tests.Select(x => x.ReflectedType.Namespace).Distinct().Count());
            Assert.AreEqual(2, runs.ElementAt(1).Tests.Select(x => x.ReflectedType.Namespace).Distinct().Count());
        }

        private IEnumerable<TestRun> GetTestRuns()
        {
            return new NamespaceSplitExtractor(_environments, AssemblyFilename, AssemblyPath, null).Execute();
        }
    }
}