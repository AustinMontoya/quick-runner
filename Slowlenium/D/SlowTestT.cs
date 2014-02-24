using NUnit.Framework;

namespace Slowlenium.D
{
    [TestFixture]
    class SlowTestT
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
