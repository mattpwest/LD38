using System.Collections.Generic;

namespace Match3.Core.Domain
{
    public class Board
    {
        public int Width { get; }
        public int Height { get; }
        private Tile[,] tiles;

        public Board(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.tiles = new Tile[Width, Height];
        }

        public Tile GetTile(int x, int y)
        {
            return this.tiles[x, y];
        }

        public void Fill(BoardGenerator boardGenerator) {
            for(int x = 0; x < this.Width; x++)
            {
                for(int y = 0; y < this.Height; y++)
                {
                    //this.tiles[x, y] = boardGenerator.GenerateTile();
                }
            }
        }

        public IList<object> GetCurrentMatches() {
            return new List<object>();
        }
    }
}
