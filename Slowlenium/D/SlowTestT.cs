using NUnit.Framework;

namespace Slowlenium.D
{
    [TestFixture]
    class SlowTestT
    {
        [Test]
        [Category("bar")]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
