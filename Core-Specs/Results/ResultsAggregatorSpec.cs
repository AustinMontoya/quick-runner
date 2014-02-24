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

        [SetUp]
        public void BeforeEach()
        {
            ResultsAggregator.Execute(Path.Combine(Environment.CurrentDirectory, Destination), _result1, _result2);
        }

        [Test]
        public void ShouldOutputAFileAtTheCorrectLocation()
        {
            Assert.IsTrue(File.Exists(Destination));
        }

        [Test]
        public void ShouldContainASingleInstanceOfEachNamespace()
        {
            var doc = XDocument.Load(Destination);
            var namespaces = doc.XPathSelectElements("//test-suite[@type=Namespace]").Select(el => el.Attribute("name").Value);
            Assert.AreEqual(namespaces.Distinct().Count(), namespaces.Count());
        }

        [Test]
        public void ShouldContainASingleInstanceOfEachFixture()
        {
            var doc = XDocument.Load(Destination);
            var fixtureNames = doc.XPathSelectElements("//test-suite[@type=TestFixture]").Select(el => el.Attribute("name").Value);
            Assert.AreEqual(fixtureNames.Distinct().Count(), fixtureNames.Count());
        }

        [Test]
        public void ShouldAddEnvironmentAttributeToAllTestCases()
        {
            var doc = XDocument.Load(Destination);
            var cases = doc.XPathSelectElements("//test-case");
            var casesWithEnvironmentAttribute = cases.Where(el => el.Attribute("qr-environment") != null);
            Assert.AreEqual(cases.Count(), casesWithEnvironmentAttribute.Count());
        }

        // TODO: Test to make sure that environment is correctly added
        [Test]
        public void ShouldHaveEnvironmentAttributeCorrectlyAssigned()
        {
            // Since there are two environments, half of the tests should have one environment,
            // which is the assertion we're making here
            var doc = XDocument.Load(Destination);
            var cases = doc.XPathSelectElements("//test-case");
            var casesWithEnvironmentAttributeSetToFoo = cases.Where(el => el.Attribute("qr-environment").Value == "foo");
            Assert.AreEqual(cases.Count() / 2, casesWithEnvironmentAttributeSetToFoo.Count());
        }
    }
}