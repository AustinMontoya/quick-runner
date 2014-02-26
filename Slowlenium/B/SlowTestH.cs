using NUnit.Framework;

namespace Slowlenium.B
{
    [TestFixture]
    class SlowTestH
    {
        [Test]
        [Category("Bar")]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
