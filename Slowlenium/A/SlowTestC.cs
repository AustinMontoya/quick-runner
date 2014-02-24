using NUnit.Framework;

namespace Slowlenium.A
{
    [TestFixture]
    public class SlowTestC
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
