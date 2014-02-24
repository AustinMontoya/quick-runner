using NUnit.Framework;

namespace Slowlenium.B
{
    [TestFixture]
    class SlowTestK
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
