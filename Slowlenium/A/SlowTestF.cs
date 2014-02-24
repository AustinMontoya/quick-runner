using NUnit.Framework;

namespace Slowlenium.A
{
    [TestFixture]
    public class SlowTestF
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
