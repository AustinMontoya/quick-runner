using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml.XPath;

namespace QuickRunner.Core.Results
{
    public static class ResultsAggregator
    {
        private class ElementEnvironment
        {
            public XElement NamespaceElement { get; set; }

            public ITestEnvironment Environment { get; set; }

            public ElementEnvironment(XElement nsElement, ITestEnvironment env)
            {
                NamespaceElement = nsElement;
                Environment = env;
            }
        }
        public static void Execute(string destPath, params TestRunResult[] results)
        {
            if (string.IsNullOrWhiteSpace(destPath))
            {
                destPath = "./TestResults.xml";
            }

            var destFolder = Path.GetDirectoryName(destPath);
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            if (results.Length < 1)
            {
                throw new ArgumentNullException("Need at least one results file to perform merge operation.");
            }

            var targetDoc = XDocument.Load(results.First().ResultsFilepath);
            var targetElementEnvironment = new ElementEnvironment(GetTestSuite(targetDoc), results.First().Environment);
            var totals = GetTotals(targetDoc);
            AddEnvironmentAttributeToCases(targetElementEnvironment.NamespaceElement, targetElementEnvironment.Environment.Name);
            foreach (var result in results.Skip(1))
            {
                var sourceDoc = XDocument.Load(result.ResultsFilepath);
                AccumulateTotals(totals, GetTotals(sourceDoc));
                var sourceElementEnvironment = new ElementEnvironment(GetTestSuite(sourceDoc), result.Environment);
                AddEnvironmentAttributeToCases(sourceElementEnvironment.NamespaceElement, sourceElementEnvironment.Environment.Name);
                MergeNamespaces(targetElementEnvironment, sourceElementEnvironment);
            }

            WriteTotals(targetDoc, totals);
            targetDoc.Save(destPath);
        }

        private static void WriteTotals(XDocument document, Dictionary<string, int> totals)
        {
            foreach (var total in totals)
            {
                document.Root.Attribute(total.Key).SetValue(total.Value);
            }
        }

        private static XElement GetTestSuite(XDocument document)
        {
            var assemblyElement =  document.Root.XPathSelectElement("test-suite");
            var setupFixtureElement = assemblyElement.XPathSelectElement("results/test-suite[@type=\"SetUpFixture\"]");
            return setupFixtureElement ?? assemblyElement;
        }

        private static void MergeNamespaces(ElementEnvironment target, ElementEnvironment source)
        {
            const string namespaceQuery = "results/test-suite[@type=\"Namespace\"]";
            
            MergeFixtures(target, source);

            var destinationChildNamespaces = target.NamespaceElement.XPathSelectElements(namespaceQuery);
            foreach (var namespaceElement in source.NamespaceElement.XPathSelectElements(namespaceQuery))
            {
                var existingNamespace =
                    destinationChildNamespaces.FirstOrDefault(
                        x => x.Attribute("name").Value == namespaceElement.Attribute("name").Value);

                if (existingNamespace == null)
                {
                    target.NamespaceElement
                        .XPathSelectElement("results")
                        .Add(namespaceElement);
                }
                else
                {
                    MergeNamespaces(new ElementEnvironment(existingNamespace, target.Environment), new ElementEnvironment(namespaceElement, source.Environment));
                }
            }
        }

        private static void MergeFixtures(ElementEnvironment target, ElementEnvironment source)
        {
            const string fixtureQuery = "results/test-suite[@type=\"TestFixture\"]";

            var destinationFixtureElements = target.NamespaceElement.XPathSelectElements(fixtureQuery);
            foreach (var fixtureElement in source.NamespaceElement.XPathSelectElements(fixtureQuery))
            {
                // for fixtures that exist in one but not the other, add them to destinationNamespace
                var existingFixture =
                    destinationFixtureElements.FirstOrDefault(
                        x => x.Attribute("name").Value == fixtureElement.Attribute("name").Value);

                if (existingFixture == null)
                {
                    target.NamespaceElement
                        .XPathSelectElement("results")
                        .Add(fixtureElement);
                }
                else
                {
                    existingFixture
                        .XPathSelectElement("results")
                        .Add(fixtureElement.XPathSelectElements("results/test-case"));
                }
            }
        }

        private static Dictionary<string, int> GetTotals(XDocument document)
        {
            var keys = new[] {"skipped", "ignored", "invalid", "errors", "inconclusive", "total", "not-run", "failures"};
            var totals = new Dictionary<string, int>();

            foreach (var key in keys)
            {
                totals[key] = int.Parse(document.Root.Attribute(key).Value);
            }

            return totals;
        }

        private static void AccumulateTotals(Dictionary<string, int> accum, Dictionary<string, int> newData)
        {
            var keys = new List<string>(accum.Keys);
            for (var i = 0; i < accum.Count; i++)
            {
                accum[keys[i]] += newData[keys[i]];
            }
        }

        private static void AddEnvironmentAttributeToCases(XElement containerElement, string environmentName)
        {
            containerElement
                .XPathSelectElements("//test-case")
                .ToList()
                .ForEach(x => x.SetAttributeValue("qr-environment", environmentName));
        }
    }
}