using NUnit.Framework;

namespace Slowlenium.D
{
    [TestFixture]
    class SlowTestX
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
