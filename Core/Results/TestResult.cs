namespace QuickRunner.Core.Results
{
    public class TestRunResult
    {
        public string ResultsFilepath { get; set; }

        public ITestEnvironment Environment { get; set; }
    }
}