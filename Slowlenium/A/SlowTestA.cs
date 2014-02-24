using NUnit.Framework;

namespace Slowlenium.A
{
    [TestFixture]
    class SlowTestA
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
