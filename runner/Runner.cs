using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QuickRunner.Core;
using QuickRunner.Core.Results;
using QuickRunner.Core.TestRunExtractors;

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
            var results = new List<TestRunResult>();
            var runs = GetRuns();

            if (runs.Count() < 1)
            {
                Console.WriteLine("No tests were found using the provided options!");
            }

            foreach (var run in GetRuns())
            {
                // Start the process               
                var starter = new NUnitProcessStarter(
                    Path.Combine(Path.GetFullPath(run.Environment.Path), 
                    Options.AssemblyFileName), 
                    run.Environment.Name);
                _processes.Add(starter);

                // Get the full type name (including namespace) for each type, join into comma-separated string
                var testNames = run.Tests.Select(x => string.Format("{0}.{1}", x.ReflectedType, x.Name));
                tasks.Add(Task.Run(
                    async () => results.Add(
                        new TestRunResult
                        {
                            ResultsFilepath = "nunit-runner\\" + await starter.RunAsync(testNames),
                            Environment = run.Environment
                        }
                    )
                ));
            }

            Task.WaitAll(tasks.ToArray());

            if (Options.AggregateResults)
            {
                ResultsAggregator.Execute(Options.ResultsFilepath, results.ToArray());
            }
        }

        private IEnumerable<TestRun> GetRuns()
        {
            return TestRunExtractorFactory.Create(Options).Execute();
        }
    }
}