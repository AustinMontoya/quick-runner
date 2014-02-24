using NUnit.Framework;

namespace Slowlenium.B
{
    [TestFixture]
    class SlowTestH
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
