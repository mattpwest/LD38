using System.Linq;
using Match3.Core.Domain;
using Match3.Core.UI.Views;

namespace Match3.Core.UI.Presenters
{
    public class BoardPresenter : IBoardPresenter
    {
        private readonly Board board;
        private readonly ITileView[,] tiles;

        private ITileView grabbedTile;
        private Move pendingMove;

        private BoardPresenter()
        {
            this.grabbedTile = null;
        }

        public BoardPresenter(ITileViewFactory tileViewFactory, IRandom random, int boardWidth, int boardHeight, params string[] tileTypes)
            : this()
        {
            var boardFactory = new RandomBoardFactory(random, tileTypes);

            this.board = boardFactory.Generate(boardWidth, boardHeight);

            this.tiles = new ITileView[boardWidth, boardHeight];

            for(int x = 0; x < this.board.Width; x++)
            {
                for(int y = 0; y < this.board.Height; y++)
                {
                    var tile = this.board.GetTile(x, y);
                    this.tiles[x, y] = tileViewFactory.CreateInitial(this, tile.Type, x, y);
                }
            }
        }

        public void Grabbed(ITileView tileView)
        {
            this.grabbedTile = tileView;
        }

        public void Moved(int x, int y)
        {
            if(x < 0 || x >= this.board.Width)
            {
                return;
            }
            if(y < 0 || y >= this.board.Height)
            {
                return;
            }

            if(this.pendingMove != null)
            {
                this.UndoPendingMove();
            }

            var tileView = this.tiles[x, y];
            this.pendingMove = new Move(x, y);
            tileView.Move(this.grabbedTile.X, this.grabbedTile.Y);
        }

        public void Released()
        {
            if(this.pendingMove == null)
            {
                this.UndoMove();
                return;
            }

            this.board.Move(this.grabbedTile.X, this.grabbedTile.Y, this.pendingMove.X, this.pendingMove.Y);

            if(!this.board.Matches.Any())
            {
                this.UndoMove();
                return;
            }

            this.board.ClearMatches();

            this.tiles[this.grabbedTile.X, this.grabbedTile.Y] = this.tiles[this.pendingMove.X, this.pendingMove.Y];
            this.tiles[this.pendingMove.X, this.pendingMove.Y] = this.grabbedTile;

            this.grabbedTile.Move(this.pendingMove.X, this.pendingMove.Y);

            this.grabbedTile = null;
            this.pendingMove = null;
        }

        private void UndoPendingMove()
        {
            var tileToUndo = this.tiles[this.pendingMove.X, this.pendingMove.Y];
            tileToUndo.Move(this.pendingMove.X, this.pendingMove.Y);
            this.pendingMove = null;
        }

        private void UndoMove()
        {
            this.grabbedTile.Move(this.grabbedTile.X, this.grabbedTile.Y);
            if(this.pendingMove != null)
            {
                this.UndoPendingMove();
            }

            this.grabbedTile = null;
        }
    }
}
