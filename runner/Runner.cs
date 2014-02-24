using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using QuickRunner.Core;
using QuickRunner.Core.Extractors;
using QuickRunner.Core.Utils;

namespace QuickRunner.Runner
{
    public class Runner
    {
        private readonly List<NUnitProcessStarter> _processes; 

        public Runner(RunnerOptions options)
        {
            Options = options;
            _processes = new List<NUnitProcessStarter>();
        }

        public RunnerOptions Options { get; private set; }

        public void Run()
        {
            // TODO: Check mode
            // Right now environment-level parallelization is only supported,
            // though with the process starter we could do more granular assembly-level parallelization as well
            var tasks = new List<Task>();
            var resultFilenames = new List<string>();

            foreach (var run in GetRuns())
            {
                // Start the process               
                var starter = new NUnitProcessStarter(Path.Combine(Path.GetFullPath(run.Environment.Path), Options.AssemblyFileName));
                _processes.Add(starter);

                // Get the full type name (including namespace) for each type, join into comma-separated string
                var testNames = run.Tests.Select(x => string.Format("{0}.{1}", x.ReflectedType, x.Name));
                tasks.Add(Task.Run(async () => resultFilenames.Add(await starter.RunAsync(testNames))));
            }

            Task.WaitAll(tasks.ToArray());

            // TODO: aggregate results?
        }

        private IEnumerable<TestRun> GetRuns()
        {
            return TestRunExtractorFactory.Create(Options).Execute();
        }
    }
}