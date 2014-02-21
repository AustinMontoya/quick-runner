namespace QuickRunner.Core.Extractors
{
    public static class TestRunExtractorFactory
    {
        public static TestRunExtractor Create(RunnerOptions options)
        {
            return new EvenSplitExtractor(
                options.Environments, 
                options.AssemblyFileName, 
                options.AssemblyPath, 
                options.ConfigFilepath);
        }
    }
}
