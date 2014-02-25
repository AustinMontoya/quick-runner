using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using QuickRunner.Core.Utils;

namespace QuickRunner.Core
{
    public class TestEnvironment
    {
        public string Name { get; set; }

        public Dictionary<string, string> AppSettings { get; set; }

        public List<string> Namespaces { get; set; } 

        public string Path { get; private set; }

        public void Initialize(string assemblyPath, string configFilepath)
        {
            Path = "environment-" + Name;

            // Clear out old environment, we want a fresh folder every time
            if (Directory.Exists(Path))
            {
                try
                {
                    Directory.Delete(Path, true);
                }
                catch
                {
                    // TODO: why?
                }
                
            }

            DirectoryUtils.CopyRecursive(new DirectoryInfo(assemblyPath).FullName, Path);

            if (string.IsNullOrEmpty(configFilepath)) return;

            // override app settings
            var envConfigPath = System.IO.Path.Combine(Path, configFilepath);

            var doc = XDocument.Load(envConfigPath);

            var appSettingsNode = doc.XPathSelectElement("//appSettings");
            var settingsNodes = appSettingsNode.XPathSelectElements("/add");
            foreach (var option in AppSettings)
            {
                var existingSetting = settingsNodes.FirstOrDefault(x => x.Attribute("key").Value == option.Key);
                if (existingSetting != null)
                {
                    existingSetting.SetAttributeValue("value", option.Value);
                }
                else
                {
                    var elem = new XElement("add");
                    elem.SetAttributeValue("key", option.Key);
                    elem.SetAttributeValue("value", option.Value);
                    appSettingsNode.Add(elem);
                }
            }

            doc.Save(envConfigPath);
        }
    }
}