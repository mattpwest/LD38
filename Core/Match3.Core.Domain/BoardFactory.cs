using System;

namespace Match3.Core.Domain
{
    public abstract class BoardFactory
    {
        public Board Board { get; private set; }

        public Board Generate(int width, int height)
        {
            this.Board = new Board(width, height);

            this.GenerateTiles(width, height, this.Board.SetTile);

            return this.Board;
        }

        protected abstract void GenerateTiles(int width, int height, Action<int, int, Tile> setTile); // Generate tiles using 
    }
}
