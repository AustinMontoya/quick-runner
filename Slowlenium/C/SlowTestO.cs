using NUnit.Framework;

namespace Slowlenium.C
{
    [TestFixture]
    class SlowTestO
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
