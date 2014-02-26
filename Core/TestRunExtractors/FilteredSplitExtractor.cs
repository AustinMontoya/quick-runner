using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QuickRunner.Core.TestRunExtractors
{
    public class FilteredSplitExtractor : TestRunExtractor
    {
        public FilteredSplitExtractor(RunnerOptions options) : base(options)
        {
        }

        protected override IEnumerable<MethodInfo> FilterTests(IEnumerable<MethodInfo> tests)
        {
            if (Options.Namespaces != null)
            {
                tests = FilterBy.Namespace(tests, Options.Namespaces);
            }

            if (Options.Categories != null)
            {
                tests = FilterBy.Category(tests, Options.Categories);
            }

            return tests;
        }
    }
}