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
            var io = Substitute.For<IOutputInputAdapter>();
            var logging = Substitute.For<ILogging>();
            var doItAll = new DoItAll(io, logging);
            var result = doItAll.Do();
            Assert.AreEqual(expected, result);
        }
        [Test, Category("Integration")]
        public void DoItAll_Does_ItAll_MatchingPasswords()
        {
            var expected = "Database.SaveToLog Exception:";
            var io = Substitute.For<IOutputInputAdapter>();
            io.GetInput().Returns("something");
            var logging = new DatabaseLogging();
            var doItAll = new DoItAll(io, logging);
            var result = doItAll.Do();
            StringAssert.Contains(expected, result);
        }
        [Test, Category("Integration")]
        public void DoItAll_Does_ItAll_MockLogging()
        {
            var expected = string.Empty;
            var io = Substitute.For<IOutputInputAdapter>();
            io.GetInput().Returns("something");
            var logging = Substitute.For<ILogging>();
            var doItAll = new DoItAll(io, logging);
            var result = doItAll.Do();
            StringAssert.Contains(expected, result);
        }
    }
}
