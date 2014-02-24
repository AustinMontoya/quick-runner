using NUnit.Framework;

namespace Slowlenium.A
{
    [TestFixture]
    class SlowTestE
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
