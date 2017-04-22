namespace Match3.Core.Domain
{
    public abstract class BoardGenerator
    {
        private Board board;

        protected void  SetTile(int x, int y)
        {
            //Board.tiles`
        }

        public abstract void Fill(Board board);
    }
}
