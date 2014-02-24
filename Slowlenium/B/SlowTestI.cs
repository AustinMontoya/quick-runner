using NUnit.Framework;

namespace Slowlenium.B
{
    [TestFixture]
    class SlowTestI
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
