namespace Match3.Core.Domain
{
    public abstract class BoardFactory
    {
        public Board Board { get; private set; }

        public Board Generate(int width, int height) {
            this.Board = new Board(width, height);

            this.GenerateTiles();

            return this.Board;
        }

        protected abstract void GenerateTiles(); // Generate tiles using 

        protected void SetTile(int x, int y, Tile tile) {
            this.Board.SetTile(x, y, tile);
        }
    }
}
