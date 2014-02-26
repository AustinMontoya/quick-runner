using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;

namespace QuickRunner.Core.Utils
{
    // based on this post here: http://blog.greatrexpectations.com/2012/08/30/deserializing-interface-properties-using-json-net/
    public class EnvironmentsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            //assume we can convert to anything for now
            return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //explicitly specify the concrete type we want to create
            var testEnvList = new List<ITestEnvironment>();
            testEnvList.AddRange(serializer.Deserialize<List<TestEnvironment>>(reader));
            return testEnvList;
        }
    }
}