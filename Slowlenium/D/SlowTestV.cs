using NUnit.Framework;

namespace Slowlenium.D
{
    [TestFixture]
    class SlowTestV
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
