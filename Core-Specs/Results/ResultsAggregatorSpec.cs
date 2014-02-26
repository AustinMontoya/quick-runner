using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml.XPath;
using NUnit.Framework;
using QuickRunner.Core;
using QuickRunner.Core.Results;

namespace Core_Specs.Results
{
    [TestFixture]
    public class ResultsAggregatorSpec
    {
        private readonly TestRunResult _result1 = new TestRunResult
        {
           ResultsFilepath = "data/results-1.xml",
           Environment = new TestEnvironment { Name = "foo" }
        };

        private readonly TestRunResult _result2 = new TestRunResult
        {
            ResultsFilepath = "data/results-2.xml",
            Environment = new TestEnvironment { Name = "bar" }
        };

        private const string Destination = "data/MergedResults.xml";

        private XDocument _resultDocument1;

        private XDocument _resultDocument2;

        private XDocument _document;

        [SetUp]
        public void BeforeEach()
        {
            ResultsAggregator.Execute(Path.Combine(Environment.CurrentDirectory, Destination), _result1, _result2);
            _document = XDocument.Load(Destination);
            _resultDocument1 = XDocument.Load(_result1.ResultsFilepath);
            _resultDocument2 = XDocument.Load(_result2.ResultsFilepath);
        }

        [Test]
        public void ShouldOutputAFileAtTheCorrectLocation()
        {
            Assert.IsTrue(File.Exists(Destination));
        }

        [Test]
        public void ShouldUpdateIgnoredTotal()
        {
            ShouldUpdateTotal("ignored");   
        }

        [Test]
        public void ShouldUpdateSkippedTotal()
        {
            ShouldUpdateTotal("skipped");
        }

        [Test]
        public void ShouldUpdateNotRunTotal()
        {
            ShouldUpdateTotal("not-run");
        }

        [Test]
        public void ShouldUpdateTotalTotal()
        {
            ShouldUpdateTotal("total");
        }

        [Test]
        public void ShouldUpdateFailuresTotal()
        {
            ShouldUpdateTotal("failures");
        }

        [Test]
        public void ShouldUpdateInconclusiveTotal()
        {
            ShouldUpdateTotal("inconclusive");
        }

        [Test]
        public void ShouldUpdateInvalidTotal()
        {
            ShouldUpdateTotal("invalid");
        }

        [Test]
        public void ShouldUpdateErrorsTotal()
        {
            ShouldUpdateTotal("errors");
        }

        [Test]
        public void ShouldContainASingleInstanceOfEachNamespace()
        {
            var namespaces = _document.XPathSelectElements("//test-suite[@type=Namespace]").Select(el => el.Attribute("name").Value);
            Assert.AreEqual(namespaces.Distinct().Count(), namespaces.Count());
        }

        [Test]
        public void ShouldContainASingleInstanceOfEachFixture()
        {
            var fixtureNames = _document.XPathSelectElements("//test-suite[@type=TestFixture]").Select(el => el.Attribute("name").Value);
            Assert.AreEqual(fixtureNames.Distinct().Count(), fixtureNames.Count());
        }

        [Test]
        public void ShouldHaveTheCombinedTestCountFromAllMergedDocuments()
        {
            var results1NumTests =_resultDocument1.XPathSelectElements("//test-case").Count();
            var results2NumTests = _resultDocument2.XPathSelectElements("//test-case").Count();
            var mergedNumTests = _document.XPathSelectElements("//test-case").Count();
            Assert.AreEqual(results1NumTests + results2NumTests, mergedNumTests);
        }

        [Test]
        public void ShouldAddEnvironmentAttributeToAllTestCases()
        {
            var cases = _document.XPathSelectElements("//test-case");
            var casesWithEnvironmentAttribute = cases.Where(el => el.Attribute("qr-environment") != null);
            Assert.AreEqual(cases.Count(), casesWithEnvironmentAttribute.Count());
        }

        // TODO: Test to make sure that environment is correctly added
        [Test]
        public void ShouldHaveEnvironmentAttributeCorrectlyAssigned()
        {
            // Since there are two environments, half of the tests should have one environment,
            // which is the assertion we're making here
            var cases = _document.XPathSelectElements("//test-case");
            var casesWithEnvironmentAttributeSetToFoo = cases.Where(el => el.Attribute("qr-environment").Value == "foo");
            Assert.AreEqual(cases.Count() / 2, casesWithEnvironmentAttributeSetToFoo.Count());
        }

        private void ShouldUpdateTotal(string attrName)
        {
            var total1 = int.Parse(XDocument.Load(_result1.ResultsFilepath).Root.Attribute(attrName).Value);
            var total2 = int.Parse(XDocument.Load(_result1.ResultsFilepath).Root.Attribute(attrName).Value);
            var mergedTotal = int.Parse(_document.Root.Attribute(attrName).Value);

            Assert.AreEqual(total1 + total2, mergedTotal);
        }
    }
}