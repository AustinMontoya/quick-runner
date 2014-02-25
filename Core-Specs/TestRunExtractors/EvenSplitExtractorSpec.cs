using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using QuickRunner.Core;
using QuickRunner.Core.TestRunExtractors;

namespace Core_Specs.TestRunExtractors
{
    [TestFixture]
    public class EvenSplitExtractorSpec
    {
        [Test]
        public void ShouldDivideTestsEvenly()
        {
             var environments = new List<TestEnvironment>
            {
                new TestEnvironment {Name = "foo"},
                new TestEnvironment {Name = "bar"},
                new TestEnvironment {Name = "baz"},
                new TestEnvironment {Name = "foz"}
            };

            var options = new RunnerOptions
            {
                AssemblyPath = "../../lib/test-assembly/",
                AssemblyFileName = "Slowlenium.dll",
                Environments = environments
            };

            var runs = new EvenSplitExtractor(options).Execute();
            Console.Write(string.Join("\n ", runs.Select(x => "Num tests for run: " + x.Tests.Count)));
            Assert.IsTrue(runs.All(x => x.Tests.Count == 6));
        }
    }
}