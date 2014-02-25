using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using QuickRunner.Core;
using QuickRunner.Core.TestRunExtractors;

namespace Core_Specs.TestRunExtractors
{
    [TestFixture]
    public class EnvironmentNamespacesExtractorSpec
    {
        private List<TestEnvironment> _environments;

        private IEnumerable<TestRun> _runs; 

        [SetUp]
        public void BeforeEach()
        {   
            _environments = new List<TestEnvironment>
            {
                new TestEnvironment {Name = "foo", Namespaces = new List<string> { "Slowlenium.A" }},
                new TestEnvironment {Name = "bar"},
            };

            var options = new RunnerOptions
            {
                Environments = _environments,
                AssemblyPath = "../../lib/test-assembly/",
                AssemblyFileName = "Slowlenium.dll"
            };

            _runs = new EnvironmentNamespacesExtractor(options).Execute();
        }

        [Test]
        public void ShouldHaveTestsInTheRun()
        {
            Assert.GreaterOrEqual(_runs.First().Tests.Count, 1);
        }

        [Test]
        public void ShouldAssignSpecifiedNamespacesToEachEnvironment()
        {
            Assert.IsTrue(_runs.First().Tests.All(m => m.DeclaringType.Namespace == "Slowlenium.A"));
        }

        [Test]
        public void ShouldNotCreateRunsForEnvironmentsWithoutANamespaceSpecifier()
        {
            Assert.AreEqual(1, _runs.Count());
        }
    }
}