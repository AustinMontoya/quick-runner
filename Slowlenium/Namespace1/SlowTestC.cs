using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Slowlenium.Namespace1
{
    [TestFixture]
    class SlowTestC : SlowTest
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
