using NUnit.Framework;

namespace Slowlenium.B
{
    [TestFixture]
    class SlowTestG
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
