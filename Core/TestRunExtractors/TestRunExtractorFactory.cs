using QuickRunner.Core.TestRunExtractors;

namespace QuickRunner.Core.Extractors
{
    public static class TestRunExtractorFactory
    {
        public static TestRunExtractor Create(RunnerOptions options)
        {
            if (options.SplitTestsBy.ToLower() == "namespace")
            {
                return new NamespaceSplitExtractor(
                    options.Environments,
                    options.AssemblyFileName,
                    options.AssemblyPath,
                    options.ConfigFilepath);
            }

            return new EvenSplitExtractor(
                options.Environments, 
                options.AssemblyFileName, 
                options.AssemblyPath, 
                options.ConfigFilepath);
        }
    }
}
