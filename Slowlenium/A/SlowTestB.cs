using NUnit.Framework;

namespace Slowlenium.A
{
    [TestFixture]
    public class SlowTestB
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}