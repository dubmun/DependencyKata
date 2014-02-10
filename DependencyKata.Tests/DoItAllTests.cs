using System.Runtime.InteropServices;
using NSubstitute;
using NUnit.Framework;

namespace DependencyKata.Tests
{
    [TestFixture]
    public class DoItAllTests
    {
        private IOutputInputAdapter _io;
        private ILogging _logging;

        [SetUp]
        public void Setup()
        {

            _io = Substitute.For<IOutputInputAdapter>();
            _logging = Substitute.For<ILogging>();
        }

        [Test, Category("Integration")]
        public void DoItAll_Does_ItAll()
        {
            var expected = "The passwords don't match";
            var doItAll = new DoItAll(_io, _logging);
            var result = doItAll.Do();
            Assert.AreEqual(expected, result);
        }
        [Test, Category("Integration")]
        public void DoItAll_Does_ItAll_MatchingPasswords()
        {
            var expected = "Database.SaveToLog Exception:";
            _io.GetInput().Returns("something");
            var logging = new DatabaseLogging();
            var doItAll = new DoItAll(_io, logging);
            var result = doItAll.Do();
            StringAssert.Contains(expected, result);
        }
        [Test]
        public void DoItAll_Does_ItAll_MockLogging()
        {
            var expected = string.Empty;
            _io.GetInput().Returns("something");
            var doItAll = new DoItAll(_io, _logging);
            var result = doItAll.Do();
            StringAssert.Contains(expected, result);
        }
    }
}
