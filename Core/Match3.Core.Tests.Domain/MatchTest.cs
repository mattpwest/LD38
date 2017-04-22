using System;
using Match3.Core.Domain;
using NUnit.Framework;

namespace Match3.Core.Tests.Domain
{
    [TestFixture]
    public class MatchTest
    {
        [Test]
        public void TestMatchCanBeCreated()
        {
            const int startX = 0;
            const int startY = 0;
            const int endX = 2;
            const int endY = 0;

            var match = new Match(startX, startY, endX, endY);

            Assert.IsNotNull(match);
            Assert.AreEqual(startX, match.StartX);
            Assert.AreEqual(startY, match.StartY);
            Assert.AreEqual(endX, match.EndX);
            Assert.AreEqual(endY, match.EndY);
        }

        [Test]
        [TestCase(0, 0, 0, 2, 0, 0)]
        [TestCase(0, 0, 0, 2, 0, 1)]
        [TestCase(0, 0, 0, 2, 0, 2)]
        [TestCase(0, 0, 2, 0, 0, 0)]
        [TestCase(0, 0, 2, 0, 1, 0)]
        [TestCase(0, 0, 2, 0, 2, 0)]
        public void TestMatchCheckCoordinateIsInMatch(int startX, int startY, int endX, int endY, int xInMatch, int yInMatch)
        {
            var match = new Match(startX, startY, endX, endY);

            var isInMatch = match.CheckInMatch(xInMatch, yInMatch);

            Assert.True(isInMatch);
        }

        [Test]
        [TestCase(0, 0, 0, 2, 1, 0)]
        [TestCase(0, 0, 0, 2, 1, 1)]
        [TestCase(0, 0, 0, 2, 1, 2)]
        [TestCase(0, 0, 2, 0, 0, 1)]
        [TestCase(0, 0, 2, 0, 1, 1)]
        [TestCase(0, 0, 2, 0, 2, 1)]
        public void TestMatchCheckCoordinateIsNotInMatch(int startX, int startY, int endX, int endY, int xNotInMatch, int yNotInMatch)
        {
            var match = new Match(startX, startY, endX, endY);

            var isInMatch = match.CheckInMatch(xNotInMatch, yNotInMatch);

            Assert.False(isInMatch);
        }

        [Test]
        public void TestMatchCoordinatesProvidedIsValid()
        {
            const int startX = 0;
            const int startY = 0;
            const int endX = 1;
            const int endY = 1;

            Assert.Throws<ArgumentException>(() => new Match(startX, startY, endX, endY));
        }

        [Test]
        [TestCase(0, 0, 0, 2, 3)]
        [TestCase(0, 0, 0, 3, 4)]
        [TestCase(0, 0, 2, 0, 3)]
        [TestCase(0, 0, 3, 0, 4)]
        public void TestMatchLengthIsCorrect(int startX, int startY, int endX, int endY, int expectedLength)
        {
            var match = new Match(startX, startY, endX, endY);

            Assert.AreEqual(expectedLength, match.Length);
        }
    }
}
