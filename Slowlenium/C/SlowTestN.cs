using NUnit.Framework;

namespace Slowlenium.C
{
    [TestFixture]
    class SlowTestN
    {
        [Test]
        [Category("bar")]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
