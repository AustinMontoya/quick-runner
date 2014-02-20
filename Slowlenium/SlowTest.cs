using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Slowlenium
{
    [TestFixture]
    public class SlowTest
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://www.google.com");
            Thread.Sleep(5000);
            driver.Quit();
        }
    }
}
