using System.Collections.Generic;
using NUnit.Framework;
using QuickRunner.Core;

namespace Core_Specs.TestRunExtractors
{
    [TestFixture]
    public class GlobalNamespaceExtractorSpec
    {
        private List<string> _namespaces;

        private IEnumerable<TestRun> _runs;
            
        [SetUp]
        public void BeforeEach()
        {
            var options = RunnerDefaults.Options;
            _namespaces = new List<string> { "Slowlenium.A", "Slowlenium.B", "Slowlenium.C", "Slowlenium.D" };
        }

        [Test]
        public void ShouldSplitNamespacesAcrossEnvironments()
        {
            
        }

        [Test]
        public void ShouldOnlyContainSpecifiedNamespaces()
        {
            
        }
    }
}