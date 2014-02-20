using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    public class NUnitProcessStarter
    {
        public string AssemblyPath { get; set; }

        public async Task<string> RunAsync(string nunitRunSpecifier)
        {
            return await Task.Run(() => RunCore(nunitRunSpecifier));
        }

        public string RunSynchronous(string nunitRunSpecifier)
        {
            return RunCore(nunitRunSpecifier);
        }

        private static string RunCore(string nunitRunSpecifier)
        {
            var resultsFilename = string.Format("results-{0}.xml", Guid.NewGuid());
            var runArg = string.Format("/run:{0}", nunitRunSpecifier);
            Console.WriteLine("Task started to run " + nunitRunSpecifier);
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"nunit-runner\nunit-console.exe",
                    WorkingDirectory = "nunit-runner",
                    Arguments = string.Join(" ",
                        runArg,
                        @"C:\quick-runner\Slowlenium\bin\Debug\Slowlenium.dll",
                        string.Format("/xml:{0}", resultsFilename)),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            };
            p.OutputDataReceived += process_OutputDataReceived;
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
            Console.WriteLine("Finished running " + nunitRunSpecifier);

            return resultsFilename;
        }

        static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}
