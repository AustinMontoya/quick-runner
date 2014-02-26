using NUnit.Framework;

namespace Slowlenium.B
{
    [TestFixture]
    class SlowTestG
    {
        [Test]
        [Category("foo")]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
