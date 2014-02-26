using System.IO;
using NUnit.Framework;
using QuickRunner.Core;

namespace Core_Specs.TestRunExtractors
{
    public class TestRunExtractorSpecBase
    {
        public RunnerOptions Options { get; private set; }

        [SetUp]
        public void BeforeEach()
        {
            Options = RunnerDefaults.GetOptions();
        }

        [TearDown]
        public void AfterEach()
        {
            foreach (var testEnvironment in Options.Environments)
            {
                var tries = 5;
                while (tries-- > 0)
                {
                    try
                    {
                        Directory.Delete(testEnvironment.Path, true);
                        break;
                    }
                    catch
                    {
                        tries--;
                    }
                }
            }
            
            
        }
    }
}