using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Runner
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
            var processStarter = new NUnitProcessStarter();
            //RunSynchronous(processStarter);
            RunAsynchronous(processStarter);
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }

        static void RunSynchronous(NUnitProcessStarter processStarter)
        {
            Profile("synchronous", () => processStarter.RunSynchronous("Slowlenium"));
        }

        static void RunAsynchronous(NUnitProcessStarter processStarter)
        {
            Profile("async", () => Task.WaitAll(
                processStarter.RunAsync("Slowlenium.A"),
                processStarter.RunAsync("Slowlenium.B"),
                processStarter.RunAsync("Slowlenium.C"),
                processStarter.RunAsync("Slowlenium.D")
                ));
        }

        static void Profile(string profileType, Action a)
        {
            var start = DateTime.Now;
            a();
            var stop = DateTime.Now;
            Console.WriteLine("{0} took {1}s", profileType, (stop - start).TotalSeconds);
        }
    }
}
