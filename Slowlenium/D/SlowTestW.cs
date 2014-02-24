using NUnit.Framework;

namespace Slowlenium.D
{
    [TestFixture]
    class SlowTestW
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
