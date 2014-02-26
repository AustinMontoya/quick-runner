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

            if (options.Namespaces != null && options.Namespaces.Count > 0)
            {
                return new GlobalNamespaceExtractor(options);
            }

            if (options.Categories != null && options.Categories.Count > 0)
            {
                return new CategoryExtractor(options);
            }

            return new EvenSplitExtractor(options);
        }
    }
}
