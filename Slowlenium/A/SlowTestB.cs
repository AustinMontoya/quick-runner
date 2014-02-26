using NUnit.Framework;

namespace Slowlenium.A
{
    [TestFixture]
    public class SlowTestB
    {
        [Test]
        [Category("bar")]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}