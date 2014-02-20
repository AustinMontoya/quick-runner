using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace Slowlenium
{
    public class WebDriverFactory
    {
        public static IWebDriver GetDriver()
        {
            var driverType = ConfigurationManager.AppSettings["driverType"];
            const string driversPath = "lib";
            switch (driverType)
            {
                case "ie":
                    return new InternetExplorerDriver(driversPath);
                    break;
                case "chrome":
                    return new ChromeDriver(driversPath);
                    break;
                default:
                    throw new Exception("Unsupported driver type: " + driverType);
            }
        }
    }
}