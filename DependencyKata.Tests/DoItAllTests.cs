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
            var expected = "The passwords don't match";
            var console = Substitute.For<IOutputInputAdapter>();
            var doItAll = new DoItAll(console);
            var result = doItAll.Do();
            Assert.AreEqual(expected, result);
        }
        [Test, Category("Integration")]
        public void DoItAll_Does_ItAll_MatchingPasswords()
        {
            var expected = "Database.SaveToLog Exception:";
            var console = Substitute.For<IOutputInputAdapter>();
            console.GetInput().Returns("something");
            var doItAll = new DoItAll(console);
            var result = doItAll.Do();
            StringAssert.Contains(expected, result);
        }
    }
}
