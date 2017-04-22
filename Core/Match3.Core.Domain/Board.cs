using System;
using System.Collections.Generic;

namespace Match3.Core.Domain
{
    public class Board
    {
        private readonly int matchLength;
        private readonly Tile[,] tiles;
        private readonly Cell[,] cells;
        private readonly List<Match> matches;

        public int Width => this.tiles.GetLength(0);
        public int Height => this.tiles.GetLength(1);
        public IEnumerable<Match> Matches => this.matches;

        private Board()
        {
            this.matchLength = 3;
            this.matches = new List<Match>();
        }

        internal Board(int width, int height)
            : this()
        {
            this.tiles = new Tile[width, height];
        }

        public Tile GetTile(int x, int y)
        {
            return this.tiles[x, y];
        }

        internal void SetTile(int x, int y, Tile tile)
        {
            this.tiles[x, y] = tile;

            this.CheckMatches(x, y);
        }

        private void CheckMatches(int x, int y)
        {
            var tileToMatch = this.tiles[x, y];
            var horizontalMatchStart = x;
            var horizontalMatchEnd = x;
            var verticalMatchStart = y;
            var verticalMatchEnd = y;

            for(var xToFind = x - 1; xToFind >= 0; xToFind--)
            {
                var tile = this.tiles[xToFind, y];
                if(tile == null || tileToMatch != tile)
                {
                    break;
                }
                horizontalMatchStart = xToFind;
            }
            for(var xToFind = x + 1; xToFind < this.Width; xToFind++)
            {
                var tile = this.tiles[xToFind, y];
                if(tile == null || tileToMatch != tile)
                {
                    break;
                }
                horizontalMatchEnd = xToFind;
            }
            for(int yToFind = y; yToFind >= 0; yToFind--) {
                var tile = this.tiles[x, yToFind];
                if (tile == null || tileToMatch != tile) {
                    break;
                }
                verticalMatchStart = yToFind;
            }
            for(int yToFind = y; yToFind < this.Height; yToFind++) {
                var tile = this.tiles[x, yToFind];
                if (tile == null || tileToMatch != tile) {
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
                        this.tiles[x, y] = null;
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

            var temp = GetTile(xStart, yStart);
            SetTile(xStart, yStart, GetTile(xEnd, yEnd));
            SetTile(xEnd, yEnd, temp);
        }

        public void FallTiles()
        {
            for(var x = 0; x < this.Width; x++)
            {
                var emptyY = -1;
                for(var y = 0; y < this.Height; y++)
                {
                    if(emptyY < 0 && this.tiles[x, y] != null)
                    {
                        continue;
                    }

                    if(emptyY < 0 && this.tiles[x, y] == null)
                    {
                        emptyY = y;
                        continue;
                    }

                    if(this.tiles[x, y] == null)
                    {
                        continue;
                    }

                    var tileToFall = this.tiles[x, y];
                    this.SetTile(x, emptyY, tileToFall);
                    this.tiles[x, y] = null;
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

            if(this.tiles[x, y] != null)
            {
                return;
            }

            this.FillTiles(tileGenerator, x, y - 1);

            this.SetTile(x, y, tileGenerator.GenerateTile());
        }
    }

    internal class Cell
    {
        private readonly int X;

        private Tile tile;
        private bool isDirty;

        public Cell()
        {
            
        }
    }

    public interface ITileGenerator
    {
        Tile GenerateTile();
    }
}
