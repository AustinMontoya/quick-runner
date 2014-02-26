using System.Collections.Generic;
using Moq;
using QuickRunner.Core;

namespace Core_Specs
{
    public static class RunnerDefaults
    {
        public const string AssemblyPath = "../../lib/test-assembly/";

        public const string AssemblyFileName = "Slowlenium.dll";

        public static List<ITestEnvironment> Environments = new List<ITestEnvironment>
        {
            CreateMockEnvironment("foo"),
            CreateMockEnvironment("bar"),
            CreateMockEnvironment("baz"),
            CreateMockEnvironment("foz")
        };

        public static RunnerOptions GetOptions()
        {
            
            return new RunnerOptions
            {
                AssemblyPath = AssemblyPath,
                AssemblyFileName = AssemblyFileName,
                Environments = Environments
            };
        }

        // TODO: Change this and move most things to DI
        public static ITestEnvironment CreateMockEnvironment(string name, List<string> namespaces = null)
        {
            var mock = Mock.Of<ITestEnvironment>();
            mock.Name = name;
            mock.Namespaces = namespaces;
            return mock;
        }
    }
}