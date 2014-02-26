using System.Collections.Generic;
using Newtonsoft.Json;
using QuickRunner.Core.Utils;

namespace QuickRunner.Core
{
    public class RunnerOptions
    {
        public string AssemblyPath { get; set; }

        public string AssemblyFileName { get; set; }

        /// <summary>
        /// Gets or sets the filepath to the config file containing the appSettings section for the assembly.
        /// The filepath should be relative to the assembly path
        /// </summary>
        public string ConfigFilepath { get; set; }

        public string SplitTestsBy { get; set; }

        public bool AggregateResults { get; set; }

        public string ResultsFilepath { get; set; }

        public List<string> Namespaces { get; set; }

        public List<string> Categories { get; set; } 

        [JsonConverter(typeof(EnvironmentsConverter))]
        public List<ITestEnvironment> Environments { get; set; }
    }
}