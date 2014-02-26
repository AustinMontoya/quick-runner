using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using QuickRunner.Core.TestRunExtractors;

namespace Core_Specs.TestRunExtractors
{
    [TestFixture]
    public class CategoryExtractorSpec
    {
        [Test]
        public void ShouldOnlyCreateRunsContainingSpecifiedCategories()
        {
            var options = RunnerDefaults.Options;
            options.Categories = new List<string> { "foo", "bar" };
            var runs = new CategoryExtractor(options).Execute();
            var attributes = 
                runs.SelectMany(r => r.Tests)
                    .SelectMany(t =>  t.GetCustomAttributes<CategoryAttribute>().Select(attr => attr.Name));

            Assert.IsTrue(attributes.All(attr => options.Categories.Contains(attr)));
        }
    }
}