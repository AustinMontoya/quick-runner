using NUnit.Framework;

namespace Slowlenium.D
{
    [TestFixture]
    class SlowTestS
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}