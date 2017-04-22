using NUnit.Framework;
using Match3.Core.Domain;
using Match3.Core.Tests.Domain.Stubs;

namespace Match3.Core.Tests.Domain {

    [TestFixture]
    public class BoardTest
    {
        [Test]
        public void TestBoardWidthAndHeightIsSetCorrecty()
        {
            // Given when
            const int width = 5;
            const int height = 5;
            var board = new StubBoardFactory().Generate(width, height);

            // Then
            Assert.AreEqual(width, board.Width);
            Assert.AreEqual(height, board.Height);
        }

        [Test]
        public void TestNewBoardIsNotEmpty()
        {
            // Given when
            const int width = 3;
            const int height = 3;
            var board = new StubBoardFactory().Generate(width, height);

            // Then
            for(var x = 0; x < width; x++)
            {
                for(var y = 0; y < height; y++)
                {
                    Assert.IsNotNull(board.GetTile(x, y));
                }
            }
        }

        [Test]
        public void TestBoardCanDetectHorizontalMatches()
        {
            // Given when
            const int width = 3;
            const int height = 3;
            AbstractBoardFactory boardFactory = new StubHorizontalMatchBoardFactory();
            var board = boardFactory.Generate(width, height);

            // Then
            Assert.AreEqual(1, board.Matches.Count);
            var match = board.Matches[0];
            Assert.AreEqual(3, match.Length);
            Assert.AreEqual(0, match.StartX);
            Assert.AreEqual(0, match.StartY);
            Assert.AreEqual(2, match.EndX);
            Assert.AreEqual(0, match.EndY);
        }

        [Test]
        public void TestBoardCanDetectVerticalMatches()
        {
            // Given when
            const int width = 3;
            const int height = 3;
            AbstractBoardFactory boardFactory = new StubVerticalMatchBoardFactory();
            var board = boardFactory.Generate(width, height);

            // Then
            Assert.AreEqual(1, board.Matches.Count);
            var match = board.Matches[0];
            Assert.AreEqual(3, match.Length);
            Assert.AreEqual(0, match.StartX);
            Assert.AreEqual(0, match.StartY);
            Assert.AreEqual(0, match.EndX);
            Assert.AreEqual(2, match.EndY);
        }
    }
}
