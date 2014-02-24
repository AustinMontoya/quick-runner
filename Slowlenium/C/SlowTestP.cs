using NUnit.Framework;

namespace Slowlenium.C
{
    [TestFixture]
    class SlowTestP
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
