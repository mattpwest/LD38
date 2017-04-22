﻿using System;
using System.Linq;
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
            BoardFactory boardFactory = new StubHorizontalMatchBoardFactory();
            var board = boardFactory.Generate(width, height);

            // Then
            Assert.AreEqual(1, board.Matches.Count());
            var match = board.Matches.First();
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
            BoardFactory boardFactory = new StubVerticalMatchBoardFactory();
            var board = boardFactory.Generate(width, height);

            // Then
            Assert.AreEqual(1, board.Matches.Count());
            var match = board.Matches.First();
            Assert.AreEqual(3, match.Length);
            Assert.AreEqual(0, match.StartX);
            Assert.AreEqual(0, match.StartY);
            Assert.AreEqual(0, match.EndX);
            Assert.AreEqual(2, match.EndY);
        }

        [Test]
        public void TestClearMatches()
        {
            // Given
            const int width = 3;
            const int height = 3;
            BoardFactory boardFactory = new StubVerticalMatchBoardFactory();
            var board = boardFactory.Generate(width, height);

            // When
            board.ClearMatches();

            // Then
            Assert.IsEmpty(board.Matches);
        }

        [Test]
        public void TestClearedMatchedTilesAreEmpty() {
            // Given
            const int width = 3;
            const int height = 3;
            BoardFactory boardFactory = new StubVerticalMatchBoardFactory();
            var board = boardFactory.Generate(width, height);
            var match = board.Matches.First();

            // When
            board.ClearMatches();

            // Then
            for(var y = match.StartY; y < match.EndY; y++)
            {
                Assert.IsNull(board.GetTile(match.StartX, y)); 
            }
        }

        [Test]
        [TestCase(0, 0, 0, 1, 1, 2)]
        [TestCase(0, 1, 0, 0, 1, 2)]
        [TestCase(0, 0, 1, 0, 2, 1)]
        [TestCase(1, 0, 0, 0, 2, 1)]
        public void TestMoveMakesMove(int xStart, int yStart, int xEnd, int yEnd, int width, int height)
        {
            BoardFactory factory = new StubBoardFactory();
            var board = factory.Generate(width, height);
            var tile1 = board.GetTile(xStart, yStart);
            var tile2 = board.GetTile(xEnd, yEnd);

            board.Move(xStart, yStart, xEnd, yEnd);

            var tileAfterMove1 = board.GetTile(xStart, yStart);
            var tileAfterMove2 = board.GetTile(xEnd, yEnd);
            Assert.AreNotEqual(tile1, tileAfterMove1);
            Assert.AreNotEqual(tile2, tileAfterMove2);
            Assert.AreEqual(tile1, tileAfterMove2);
            Assert.AreEqual(tile2, tileAfterMove1);
        }

        [Test]
        [TestCase(0, 0, 0, 2, 2, 3, "Tiles must be adjacent")]
        [TestCase(0, 2, 0, 0, 2, 3, "Tiles must be adjacent")]
        [TestCase(0, 1, 1, 0, 2, 2, "Tiles can not be diagonal")]
        [TestCase(1, 0, 0, 1, 2, 2, "Tiles can not be diagonal")]
        [TestCase(0, 0, 1, 1, 2, 2, "Tiles can not be diagonal")]
        [TestCase(1, 1, 0, 0, 2, 2, "Tiles can not be diagonal")]
        public void TestMoveFailsIfNotAdjacent(int xStart, int yStart, int xEnd, int yEnd, int width, int height, string message) {
            BoardFactory factory = new StubBoardFactory();
            var board = factory.Generate(width, height);

            Assert.Throws<InvalidOperationException>(() => board.Move(xStart, yStart, xEnd, yEnd), message);
        }
    }
}