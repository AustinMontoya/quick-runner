using System.Linq;

namespace QuickRunner.Core.TestRunExtractors
{
    public static class TestRunExtractorFactory
    {
        public static TestRunExtractor Create(RunnerOptions options)
        {
            if (options.Environments.Any(env => env.Namespaces != null))
            {
                return new EnvironmentNamespacesExtractor(options);
            }

            if (options.Namespaces.Count > 0)
            {
                return new GlobalNamespaceExtractor(options);
            }

            return new EvenSplitExtractor(options);
        }
    }
}
