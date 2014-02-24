using NUnit.Framework;

namespace Slowlenium.C
{
    [TestFixture]
    class SlowTestR
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
