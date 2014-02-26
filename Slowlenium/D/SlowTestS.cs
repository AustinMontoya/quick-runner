using NUnit.Framework;

namespace Slowlenium.D
{
    [TestFixture]
    class SlowTestS
    {
        [Test]
        [Category("foo")]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}