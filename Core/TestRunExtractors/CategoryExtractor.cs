using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace QuickRunner.Core.TestRunExtractors
{
    public class CategoryExtractor : TestRunExtractor
    {
        public List<string> Categories { get; private set; }
 
        public CategoryExtractor(RunnerOptions options) : base(options)
        {
            Categories = options.Categories ?? new List<string>();
        }

        protected override IEnumerable<MethodInfo> FilterTests(IEnumerable<MethodInfo> tests)
        {
            return tests
                .Where(t => HasCustomAttribute(t, typeof (CategoryAttribute)))
                .Where(HasMatchingCategory);
        }

        private bool HasMatchingCategory(MethodInfo test)
        {
            return test.GetCustomAttributes<CategoryAttribute>()
                .Any(attr => Categories.Contains(attr.Name));
        }
    }
}