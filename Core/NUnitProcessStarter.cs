using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRunner.Core
{
    public class NUnitProcessStarter
    {
        public string AssemblyPath { get; set; }
        public string EnvironemntName { get; set; }

        public NUnitProcessStarter(string assemblyPath, string environemntName)
        {
            AssemblyPath = assemblyPath;
            EnvironemntName = environemntName;
        }

        public async Task<string> RunAsync(string nunitRunSpecifier)
        {
            return await RunCore(nunitRunSpecifier);
        }

        public async Task<string> RunAsync(IEnumerable<string> nunitRunSpecifiers)
        {
            var combinedSpecifiers = string.Join(",", nunitRunSpecifiers);
            return await RunCore(combinedSpecifiers);
        }

        private async Task<string> RunCore(string nunitRunSpecifier)
        {
            return await Task.Run(() =>
            {
                var resultsFilename = string.Format("results-{0}.xml", EnvironemntName);
                var runArg = string.Format("/run:{0}", nunitRunSpecifier);
                const string frameworkArg = "/framework:net-4.5.1";
                Console.WriteLine("Tests started to run: \n" + string.Join("\n", nunitRunSpecifier.Split(',').Select(s => "- " + s)));
                var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"nunit-runner\nunit-console-x86.exe", // TODO: why x86 for PlatformSpecs? Maybe should get this from assembly while grabbing test names
                        WorkingDirectory = "nunit-runner",
                        Arguments = string.Join(" ",
                            runArg,
                            AssemblyPath,
                            frameworkArg,
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
            });
        }

        static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}
