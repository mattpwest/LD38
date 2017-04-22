using NUnit.Framework;
using Match3.Core.Domain;
using Match3.Core.Tests.Domain.Stubs;

namespace Match3.Core.Tests.Domain {
    
    [TestFixture]
    public class BoardTest {

        [Test]
        public void TestBoardCanBeCreated()
        {
            Board board = new Board(1, 1);

            Assert.IsNotNull(board);
        }

        [Test]
        public void TestBoardWidthAndHeightIsSetCorrecty()
        {
            var width = 5;
            var height = 5;

            var board = new Board(width, height);

            Assert.AreEqual(width, board.Width);
            Assert.AreEqual(height, board.Height);
        }

        [Test]
        public void TestNewBoardIsEmpty()
        {
            const int width = 3;
            const int height = 3;
            var board = new Board(width, height);

            for(var x = 0; x < width; x++)
            {
                for(var y = 0; y < height; y++)
                {
                    Assert.IsNull(board.GetTile(x, y));
                }
            }
        }

        [Test]
        public void TestFillBoardAllTilesAreSet()
        {
            const int width = 3;
            const int height = 3;
            var board = new Board(width, height);
            //ITileGenerator tileGenerator = new StubBoardGenerator();

            //board.Fill(tileGenerator);

            //for (var x = 0; x < width; x++) {
            //    for (var y = 0; y < height; y++) {
            //        Assert.IsNotNull(board.GetTile(x, y));
            //    }
            //}
        }

        [Test]
        public void TestFillBoardGeneratesBoardWithNoMatches()
        {
            const int width = 3;
            const int height = 3;
            var board = new Board(width, height);
            //ITileGenerator tileGenerator = new StubBoardGenerator();

            //board.Fill(tileGenerator);

            //Assert.AreEqual(0, board.GetCurrentMatches().Count);
        }

        /*        [Test]
                public void TestMatchIsDetected()
                {
                    const int width = 3;
                    const int height = 3;
                    var board = new Board(width, height);
                    ITileGenerator tileGenerator = new StubBoardGenerator();

                    board.Fill(tileGenerator);

                    board.Set

                    Assert.AreEqual(0, board.GetCurrentMatches().Count);
                }*/
    }
}
