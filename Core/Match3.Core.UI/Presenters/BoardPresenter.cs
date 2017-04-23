using System;
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
            var tileGenerator = new RandomTileGenerator(random, tileTypes);
            var boardFactory = new RandomBoardFactory(tileGenerator);

            this.board = boardFactory.Generate(boardWidth, boardHeight);
            this.board.CheckMatches();

            while(this.board.Matches.Any())
            {
                this.board.ClearMatches();

                this.board.FallTiles();

                this.board.FillTiles(tileGenerator);

                this.board.CheckMatches();
            }

            this.tiles = new ITileView[boardWidth, boardHeight];

            for(int x = 0; x < this.board.Width; x++)
            {
                for(int y = 0; y < this.board.Height; y++)
                {
                    var tile = this.board.GetTile(x, y);
                    this.tiles[x, y] = tileViewFactory.CreateInitial(this, tile.Type, x, y);
                    this.tiles[x, y].Fall(x, y);
                }
            }
        }

        public void Grabbed(ITileView tileView)
        {
            if(this.grabbedTile != null)
            {
                return;
            }

            this.grabbedTile = tileView;
        }

        public void Moved(ITileView callingTileView, int x, int y)
        {
            if(this.grabbedTile != callingTileView)
            {
                return;
            }
            if(x < 0 || x >= this.board.Width)
            {
                return;
            }
            if(y < 0 || y >= this.board.Height)
            {
                return;
            }
            if(x == this.grabbedTile.X && y == this.grabbedTile.Y)
            {
                return;
            }

            if(this.pendingMove != null)
            {
                var tileToUndo = this.tiles[this.pendingMove.FromX, this.pendingMove.FromY];
                tileToUndo.Move(this.pendingMove.ToX, this.pendingMove.ToY);
                this.tiles[this.pendingMove.ToX, this.pendingMove.ToY] = tileToUndo;
                this.grabbedTile.Move(this.pendingMove.FromX, this.pendingMove.FromY);
                this.tiles[this.pendingMove.FromX, this.pendingMove.FromY] = this.grabbedTile;
                this.pendingMove = null;
                return;
            }

            this.pendingMove = new Move(x, y, this.grabbedTile.X, this.grabbedTile.Y);
            var tileView = this.tiles[this.pendingMove.ToX, this.pendingMove.ToY];
            tileView.Move(this.pendingMove.FromX, this.pendingMove.FromY);
            this.tiles[this.pendingMove.FromX, this.pendingMove.FromY] = tileView;
            this.grabbedTile.Move(this.pendingMove.ToX, this.pendingMove.ToY);
            this.tiles[this.pendingMove.ToX, this.pendingMove.ToY] = this.grabbedTile;
        }

        public void Released(ITileView callingTileView)
        {
            if(this.grabbedTile != callingTileView)
            {
                return;
            }

            //if(this.pendingMove == null)
            //{
            //    this.grabbedTile = null;
            //    return;
            //}

            this.board.Move(this.pendingMove.FromX, this.pendingMove.FromY, this.pendingMove.ToX, this.pendingMove.ToY);

            this.board.CheckMatches(); 

            if (!this.board.Matches.Any()) {
                var tileToUndo = this.tiles[this.pendingMove.FromX, this.pendingMove.FromY];
                tileToUndo.Move(this.pendingMove.ToX, this.pendingMove.ToY);
                this.tiles[this.pendingMove.ToX, this.pendingMove.ToY] = tileToUndo;
                this.grabbedTile.Move(this.pendingMove.FromX, this.pendingMove.FromY);
                this.tiles[this.pendingMove.FromX, this.pendingMove.FromY] = this.grabbedTile;
            }

            foreach(var matchedCell in this.board.Matches.SelectMany(x => x.MatchedCells))
            {
                this.tiles[matchedCell.X, matchedCell.Y].Destroy();
            }

            this.board.ClearMatches();

            var falls = this.board.FallTiles();

            foreach(var fall in falls)
            {
                var tileViewToFall = this.tiles[fall.From.X, fall.From.Y];
                this.tiles[fall.To.X, fall.To.Y] = tileViewToFall;
                tileViewToFall.Fall(fall.To.X, fall.To.Y);
            }

            //this.tiles[this.grabbedTile.X, this.grabbedTile.Y] = this.tiles[this.pendingMove.ToX, this.pendingMove.ToY];
            //this.tiles[this.pendingMove.ToX, this.pendingMove.ToY] = this.grabbedTile;

            //this.grabbedTile.Move(this.pendingMove.ToX, this.pendingMove.ToY);

            this.grabbedTile = null;
            this.pendingMove = null;
        }

        private void UndoPendingMove()
        {
            var tileToUndo = this.tiles[this.pendingMove.FromX, this.pendingMove.FromY];
            tileToUndo.Move(this.pendingMove.ToX, this.pendingMove.ToY);
            this.grabbedTile.Move(this.pendingMove.FromX, this.pendingMove.FromY);
            this.pendingMove = null;
        }
    }
}
