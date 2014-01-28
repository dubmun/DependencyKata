using NSubstitute;
using NUnit.Framework;

namespace DependencyKata.Tests
{
    [TestFixture]
    public class DoItAllTests
    {
        [Test, Category("Integration")]
        public void DoItAll_Does_ItAll()
        {
            var console = Substitute.For<IConsoleAdapter>();
            var doItAll = new DoItAll(console);
            doItAll.Do();
        }
    }
}
