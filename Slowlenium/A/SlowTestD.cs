using NUnit.Framework;

namespace Slowlenium.A
{
    [TestFixture]
    class SlowTestD
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
