using System.Collections.Generic;

namespace Match3.Core.Domain
{
    public class Board
    {
        private readonly int matchLength;

        private readonly Tile[,] tiles;
        public int Width => this.tiles.GetLength(0);
        public int Height => this.tiles.GetLength(1);
        public List<Match> Matches { get; }

        private Board()
        {
            this.matchLength = 3;
            this.Matches = new List<Match>();
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
                this.Matches.Add(horizontalMatch);
            }

            if(verticalMatchEnd - verticalMatchStart + 1 >= this.matchLength)
            {
                var verticalMatch = new Match(x, verticalMatchStart, x, verticalMatchEnd);
                this.Matches.Add(verticalMatch);
            }
        }
    }
}
