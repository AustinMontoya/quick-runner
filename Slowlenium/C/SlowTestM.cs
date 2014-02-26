using NUnit.Framework;

namespace Slowlenium.C
{
    [TestFixture]
    class SlowTestM
    {
        [Test]
        [Category("foo")]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
