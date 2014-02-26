using System;
using System.Collections.Generic;
using NUnit.Framework;
using QuickRunner.Core;

namespace Core_Specs
{
    public static class RunnerDefaults
    {
        public const string AssemblyPath = "../../lib/test-assembly/";

        public const string AssemblyFileName = "Slowlenium.dll";

        public static List<TestEnvironment> Environments = new List<TestEnvironment>
        {
            new TestEnvironment { Name = "foo" },
            new TestEnvironment { Name = "bar" },
            new TestEnvironment { Name = "baz" },
            new TestEnvironment { Name = "foz" }
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
    }
}