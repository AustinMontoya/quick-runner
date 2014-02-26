using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using QuickRunner.Core;
using QuickRunner.Core.TestRunExtractors;

namespace Core_Specs.TestRunExtractors
{
    [TestFixture]
    public class FilteredSplitExtractorSpec : TestRunExtractorSpecBase
    {
        private static readonly List<string> Namespaces = new List<string> { "Slowlenium.A", "Slowlenium.C" };

        private static readonly List<String> Categories = new List<string> { "foo" };

        [Test]
        public void ShouldOnlyContainSpecifiedNamespaces()
        {
            Options.Namespaces = Namespaces;
            AssertNamespaces(new FilteredSplitExtractor(Options).Execute());
        }

        [Test]
        public void ShouldContainSubNamespaces()
        {
            Options.Namespaces = new List<string> { "Slowlenium" };
            var runs = new FilteredSplitExtractor(Options).Execute();
            Assert.AreEqual(24, runs.SelectMany(r => r.Tests).Count());
        }

        [Test]
        public void ShouldOnlyCreateRunsContainingSpecifiedCategories()
        {
            Options.Categories = Categories;
            AssertCategories(new FilteredSplitExtractor(Options).Execute());
        }

        [Test]
        public void ShouldFilterByNamespaceAndCategoryWhenBothArePresent()
        {
            Options.Categories = Categories;
            Options.Namespaces = Namespaces;
            var runs = new FilteredSplitExtractor(Options).Execute();

            AssertNamespaces(runs);
            AssertCategories(runs);
        }

        private void AssertCategories(IEnumerable<TestRun> runs)
        {
            var categories =
                runs.SelectMany(r => r.Tests)
                    .SelectMany(t => t.GetCustomAttributes<CategoryAttribute>().Select(attr => attr.Name));

            Assert.IsTrue(categories.All(c => Categories.Contains(c)));
        }

        private void AssertNamespaces(IEnumerable<TestRun> runs)
        {
            Assert.IsTrue(runs.SelectMany(r => r.Tests).All(t => Namespaces.Contains(t.ReflectedType.Namespace)));
        }
    }
}