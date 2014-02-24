using System;
using System.Diagnostics;
using System.IO;
using NDesk.Options;
using Newtonsoft.Json;
using QuickRunner.Core;

namespace QuickRunner.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
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

        static void StartWatchdog()
        {
            Process.Start("QuickRunner.Watchdog.exe", Process.GetCurrentProcess().Id.ToString());
        }
    }
}
