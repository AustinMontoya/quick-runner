using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace QuickRunner.Core.Extractors
{
    public abstract class TestRunExtractor
    {
        protected Assembly Assembly { get; private set; }

        protected List<TestEnvironment> Environments { get; private set; }

        private readonly string _assemblyPath;

        private readonly string _assemblyFilename;

        protected TestRunExtractor(IEnumerable<TestEnvironment> environments, string assemblyFilename, string assemblyPath, string configFilepath)
        {
            Environments = environments.ToList();
            _assemblyFilename = assemblyFilename;
            _assemblyPath = assemblyPath;
            Environments.ForEach(env => env.Initialize(assemblyPath, configFilepath));
        }

        public IEnumerable<TestRun> Execute()
        {
            LoadAssembly();
            return GetRuns();
        }

        protected abstract IEnumerable<TestRun> GetRuns();

        private void LoadAssembly()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ExternalAssemblyResolver; // Temporarily enhance dependency resolution
            Assembly = Assembly.LoadFrom(Path.Combine(_assemblyPath, _assemblyFilename));
            AppDomain.CurrentDomain.AssemblyResolve -= ExternalAssemblyResolver;
        }

        private Assembly ExternalAssemblyResolver(object sender, ResolveEventArgs args)
        {
            try
            {
                // Grab GAC or currently loaded assembly
                return Assembly.Load(args.Name);
            }
            catch
            {
                // Grab platform-specific assembly
                return Assembly.LoadFrom(Path.Combine(_assemblyPath, args.Name + ".dll"));
            }
        }
    }
}