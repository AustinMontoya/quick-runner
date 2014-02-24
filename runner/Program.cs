using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using NDesk.Options;
using Newtonsoft.Json;
using QuickRunner.Core;

namespace QuickRunner.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 1) Divide tests up to run on separate threads
            * How?
            * - By just number of tests
            * - By category
            * - By namespace
            * Let testGroups = divided Tests
            * Let runners = List; maxLength = number of threads to run on
            * 2) while runners.count < runners.maxLength
            *       Push testGroups[i] into runner
            *       mark open runner as busy
            * 3) when runner is marked as busy:
            *       spawn runner, wait for completion
            *       collect xml results
            *       mark runner as not busy
            * 4) aggregate results into single document
            *    write document to disk
            */
            var runner = new Runner(GetOptions(args));
            StartWatchdog();
            runner.Run();

            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }

        private static RunnerOptions GetOptions(string[] args)
        {
            var configPath = string.Empty;
            RunnerOptions options;

            new OptionSet
            {
                {"config=", v => configPath = v}
            }.Parse(args);
            
            using (var sr = new StreamReader(configPath))
            {
                options = JsonConvert.DeserializeObject<RunnerOptions>(sr.ReadToEnd());
            }

            return options;
        }

        static void Profile(string profileType, Action a)
        {
            var start = DateTime.Now;
            a();
            var stop = DateTime.Now;
            Console.WriteLine("{0} took {1}s", profileType, (stop - start).TotalSeconds);
        }

        static void StartWatchdog()
        {
            Process.Start("QuickRunner.Watchdog.exe", Process.GetCurrentProcess().Id.ToString());
        }
    }
}
