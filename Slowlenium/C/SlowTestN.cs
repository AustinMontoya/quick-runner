using NUnit.Framework;

namespace Slowlenium.C
{
    [TestFixture]
    class SlowTestN
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
