using NUnit.Framework;

namespace Slowlenium.B
{
    [TestFixture]
    class SlowTestJ
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
