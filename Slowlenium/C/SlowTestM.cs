using NUnit.Framework;

namespace Slowlenium.C
{
    [TestFixture]
    class SlowTestM
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
