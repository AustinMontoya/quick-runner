using System;
using System.Collections.Generic;
using System.Reflection;

namespace QuickRunner.Core
{
    public class TestRun
    {
        public TestRun(TestEnvironment environment, IEnumerable<MethodInfo> tests)
        {
            Environment = environment;
            Tests = tests;
        }

        public TestEnvironment Environment { get; set; }

        public IEnumerable<MethodInfo> Tests { get; private set; }
    }
}