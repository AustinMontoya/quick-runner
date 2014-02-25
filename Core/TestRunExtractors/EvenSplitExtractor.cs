using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace QuickRunner.Core.TestRunExtractors
{
    /// <summary>
    /// Creates m test runs of n tests,
    /// where m is the number of environments
    /// and n is the total number of tests divided by m
    /// </summary>
    public class EvenSplitExtractor : TestRunExtractor
    {
        public EvenSplitExtractor(RunnerOptions options) 
            : base(options)
        {
        }

        protected override IEnumerable<MethodInfo> FilterTests(IEnumerable<MethodInfo> tests)
        {
            return tests;
        }
    }
}