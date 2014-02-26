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
    }
}