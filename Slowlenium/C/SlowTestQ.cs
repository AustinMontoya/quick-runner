using NUnit.Framework;

namespace Slowlenium.C
{
    [TestFixture]
    class SlowTestQ
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
