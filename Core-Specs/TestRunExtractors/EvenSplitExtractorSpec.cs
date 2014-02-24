using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using QuickRunner.Core;
using QuickRunner.Core.Extractors;

namespace Core_Specs.TestRunExtractors
{
    [TestFixture]
    public class EvenSplitExtractorSpec
    {
        private const string AssemblyPath = "../../lib/test-assembly/";
        private const string AssemblyFilename = "Slowlenium.dll";

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

            var runs = new EvenSplitExtractor(environments, AssemblyFilename, AssemblyPath, null).Execute();
            Console.Write(string.Join("\n ", runs.Select(x => "Num tests for run: " + x.Tests.Count)));
            Assert.IsTrue(runs.All(x => x.Tests.Count == 6));
        }
    }
}