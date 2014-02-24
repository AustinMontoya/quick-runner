using NUnit.Framework;

namespace Slowlenium.D
{
    [TestFixture]
    class SlowTestU
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
