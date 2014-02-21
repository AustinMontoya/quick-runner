using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace QuickRunner.Core.Extractors
{
    /// <summary>
    /// Creates m test runs of n tests,
    /// where m is the number of environments
    /// and n is the total number of tests divided by m
    /// </summary>
    public class EvenSplitExtractor : TestRunExtractor
    {
        public EvenSplitExtractor(List<TestEnvironment> environments, string assemblyFilename, string assemblyPath, string configFilepath) 
            : base(environments, assemblyFilename, assemblyPath, configFilepath)
        {
        }

        protected override IEnumerable<TestRun> GetRuns()
        {
            var classes = (
                from type in Assembly.GetTypes()
                let attrs = type.GetCustomAttributes(typeof(TestFixtureAttribute), true)
                where attrs != null && attrs.Length > 0
                select type);

            var tests = (
                from method in classes.SelectMany(x => x.GetMethods())
                let attrs = method.GetCustomAttributes(typeof(TestAttribute), true)
                where attrs != null && attrs.Length > 0
                select method).ToList();


            var numGroups = Environments.Count;
            var i = 0;
            return tests
                .GroupBy(x => i++ % numGroups)
                .Zip(Environments, (testGroup, environment) => new TestRun(environment, testGroup));
        }
    }
}