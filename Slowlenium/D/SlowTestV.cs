using NUnit.Framework;

namespace Slowlenium.D
{
    [TestFixture]
    [Category("meow")]
    class SlowTestV
    {
        [Test]
        public void IShouldOpenABrowserAndDoSomeStuffThatTakesAWhile()
        {
            SlowTest.DoStuff();
        }
    }
}
