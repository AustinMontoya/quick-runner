using NUnit.Framework;

namespace Slowlenium.B
{
    [TestFixture]
    class SlowTestL
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
