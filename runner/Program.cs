using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
            var p = new Process();
            p.StartInfo = new ProcessStartInfo
            {
                FileName = @"nunit-runner\nunit-console.exe",
                WorkingDirectory = "nunit-runner",
                Arguments = string.Join(" ",
                    "/run:Specs.Capa",
                    @"C:\grc\core\Specs\bin\Debug\TNW.Core-Specs.dll",
                    string.Format("/xml:results-{0}.xml", Guid.NewGuid())),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true
            };

            p.OutputDataReceived += process_OutputDataReceived;
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
        }

        static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

    }
}
