using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using QuickRunner.Core;
using QuickRunner.Core.TestRunExtractors;

namespace Core_Specs.TestRunExtractors
{
    [TestFixture]
    public class EvenSplitExtractorSpec : TestRunExtractorSpecBase
    {
        [Test]
        public void ShouldDivideTestsEvenly()
        {
            var runs = new EvenSplitExtractor(Options).Execute();
            var firstLength = runs.First().Tests.Count;
            Assert.IsTrue(runs.All(r => r.Tests.Count == firstLength));
        }
    }
}