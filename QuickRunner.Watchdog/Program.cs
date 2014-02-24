using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickRunner.Watchdog
{
    /// <summary>
    /// This is a simple program that's used to make sure that spawned nunit instances are cleaned up on
    /// unexpected exit (e.g. task runner kill or ctrl+c). I know this is ghetto and virus-like, don't judge me!
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int pid;

            if (!(args.Length == 1 && int.TryParse(args[0], out pid)))
            {
                throw new ArgumentException("Missing or invalid process id.");
            }

            var process = Process.GetProcessById(pid);
            while (!process.HasExited)
            {
                Thread.Sleep(1000);
            }

            KillAllTheThings();
        }

        private static void KillAllTheThings()
        {
            // Naive implementation: kill nunit-console and nunit-agent
            // TODO: Figure out how to detect individual console and agent instances and kill them per ProcessStarter
            var processes = Process.GetProcesses()
                .Where(x => x.ProcessName.Contains("nunit"))
                .ToList();
            processes.ForEach(p => p.Kill());
        }
    }
}
