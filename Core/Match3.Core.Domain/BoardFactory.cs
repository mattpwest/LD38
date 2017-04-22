using System;

namespace Match3.Core.Domain
{
    public abstract class BoardFactory
    {
        public Board Generate(int width, int height)
        {
            var board = new Board(width, height);

            this.GenerateTiles(width, height, board.SetTile);

            return board;
        }

        protected abstract void GenerateTiles(int width, int height, Action<int, int, Tile> setTile); // Generate tiles using 
    }
}
