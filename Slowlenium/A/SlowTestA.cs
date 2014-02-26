using NUnit.Framework;

namespace Slowlenium.A
{
    [TestFixture]
    class SlowTestA
    {
        [Test]
        [Category("foo")]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
