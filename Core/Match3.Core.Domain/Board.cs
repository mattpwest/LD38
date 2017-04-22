﻿using System;
using System.Collections.Generic;

namespace Match3.Core.Domain
{
    public class Board
    {
        private readonly int matchLength;
        private readonly Cell[,] cells;
        private readonly List<Match> matches;

        public int Width => this.cells.GetLength(0);
        public int Height => this.cells.GetLength(1);
        public IEnumerable<Match> Matches => this.matches;

        private Board()
        {
            this.matchLength = 3;
            this.matches = new List<Match>();
        }

        internal Board(int width, int height)
            : this()
        {
            this.cells = new Cell[width, height];

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    this.cells[x, y] = new Cell(x, y);
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return this.cells[x, y].Tile;
        }

        internal void SetTile(int x, int y, Tile tile)
        {
            this.cells[x, y].Tile = tile;

            this.CheckMatches(x, y);
        }

        private void CheckMatches(int x, int y)
        {
            var tileToMatch = this.cells[x, y].Tile;
            var horizontalMatchStart = x;
            var horizontalMatchEnd = x;
            var verticalMatchStart = y;
            var verticalMatchEnd = y;

            for(var xToFind = x - 1; xToFind >= 0; xToFind--)
            {
                var cell = this.cells[xToFind, y];
                if(cell.IsEmpty || tileToMatch != cell.Tile)
                {
                    break;
                }
                horizontalMatchStart = xToFind;
            }
            for(var xToFind = x + 1; xToFind < this.Width; xToFind++)
            {
                var cell = this.cells[xToFind, y];
                if(cell.IsEmpty || tileToMatch != cell.Tile)
                {
                    break;
                }
                horizontalMatchEnd = xToFind;
            }
            for(int yToFind = y; yToFind >= 0; yToFind--) {
                var cell = this.cells[x, yToFind];
                if (cell.IsEmpty || tileToMatch != cell.Tile) {
                    break;
                }
                verticalMatchStart = yToFind;
            }
            for(int yToFind = y; yToFind < this.Height; yToFind++) {
                var cell = this.cells[x, yToFind];
                if (cell.IsEmpty || tileToMatch != cell.Tile) {
                    break;
                }
                verticalMatchEnd = yToFind;
            }

            if(horizontalMatchEnd - horizontalMatchStart + 1 >= this.matchLength)
            {
                 var horizontalMatch = new Match(horizontalMatchStart, y, horizontalMatchEnd, y);
                this.matches.Add(horizontalMatch);
            }

            if(verticalMatchEnd - verticalMatchStart + 1 >= this.matchLength)
            {
                var verticalMatch = new Match(x, verticalMatchStart, x, verticalMatchEnd);
                this.matches.Add(verticalMatch);
            }
        }

        public void ClearMatches()
        {
            foreach(var match in this.Matches)
            {
                for(int x = match.StartX; x <= match.EndX; x++)
                {
                    for(int y = match.StartY; y <= match.EndY; y++)
                    {
                        this.cells[x, y].Clear();
                    }
                }
            }

            this.matches.Clear();
        }

        public void Move(int xStart, int yStart, int xEnd, int yEnd)
        {
            var xDiff = Math.Abs(xEnd - xStart);
            var yDiff = Math.Abs(yEnd - yStart);
            if(xDiff > 1 || yDiff > 1)
            {
                throw new InvalidOperationException("Tiles must be adjacent");
            }
            if(xDiff > 0 && yDiff > 0)
            {
                throw new InvalidOperationException("Tiles can not be diagonal");
            }

            var temp = this.cells[xStart, yStart].Tile;
            this.cells[xStart, yStart].Tile = this.cells[xEnd, yEnd].Tile;
            this.cells[xEnd, yEnd].Tile = temp;
        }

        public void FallTiles()
        {
            for(var x = 0; x < this.Width; x++)
            {
                var emptyY = -1;
                for(var y = 0; y < this.Height; y++)
                {
                    if(emptyY < 0 && !this.cells[x, y].IsEmpty)
                    {
                        continue;
                    }

                    if(emptyY < 0 && this.cells[x, y].IsEmpty)
                    {
                        emptyY = y;
                        continue;
                    }

                    if(this.cells[x, y].IsEmpty)
                    {
                        continue;
                    }

                    var tileToFall = this.cells[x, y].Tile;
                    this.cells[x, emptyY].Tile = tileToFall;
                    this.cells[x, y].Clear();
                    emptyY++;
                }
            }
        }

        public void FillTiles(ITileGenerator tileGenerator)
        {
            this.FillTiles(tileGenerator, 0);
        }

        private void FillTiles(ITileGenerator tileGenerator, int x)
        {
            if(x >= this.Width)
            {
                return;
            }

            this.FillTiles(tileGenerator, x, this.Height - 1);

            this.FillTiles(tileGenerator, x + 1);
        }

        private void FillTiles(ITileGenerator tileGenerator, int x, int y)
        {
            if(y < 0)
            {
                return;
            }

            if(!this.cells[x, y].IsEmpty)
            {
                return;
            }

            this.FillTiles(tileGenerator, x, y - 1);

            this.SetTile(x, y, tileGenerator.GenerateTile());
        }
    }

    public interface ITileGenerator
    {
        Tile GenerateTile();
    }
}
