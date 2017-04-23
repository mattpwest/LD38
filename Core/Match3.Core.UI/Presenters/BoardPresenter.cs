using System.Collections.Generic;
using Match3.Core.Domain;
using Match3.Core.UI.Views;

namespace Match3.Core.UI.Presenters
{
    public class BoardPresenter : IBoardPresenter
    {
        private readonly IScoreView scoreView;
        private readonly ITileViewFactory tileViewFactory;
        private readonly ITileView[,] tiles;
        private readonly HashSet<ITileView> fallingTiles;

        private readonly Board board;
        private readonly ITileGenerator tileGenerator;
        private readonly int tileMatchValue;
        private readonly Scoring scoring;

        private ITileView grabbedTile;
        private Move pendingMove;

        private BoardPresenter()
        {
            this.tileMatchValue = 60;

            this.grabbedTile = null;
            this.fallingTiles = new HashSet<ITileView>();
        }

        public BoardPresenter(
            IScoreView scoreView, 
            ITileViewFactory tileViewFactory, 
            IRandom random,
            int boardWidth,
            int boardHeight,
            params string[] tileTypes)
            : this()
        {
            this.scoreView = scoreView;
            this.tileViewFactory = tileViewFactory;
            var tileGenerator = new RandomTileGenerator(random, tileTypes);
            this.tileGenerator = tileGenerator;
            var boardFactory = new RandomBoardFactory(tileGenerator);

            this.board = boardFactory.Generate(boardWidth, boardHeight);
            this.board.CheckMatches();

            while(this.board.HasMatches)
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
                    this.fallingTiles.Add(this.tiles[x, y]);
                }
            }


            this.scoring = new Scoring(0, 0, 10);
            this.scoreView.SetMoves(this.scoring.MovesAllowed - this.scoring.MovesMade);
            this.scoreView.SetScore(this.scoring.CurrentScore);
            this.scoreView.SetMatches(this.scoring.CurrentMatches);
        }

        public void Grabbed(ITileView tileView)
        {
            if(this.grabbedTile != null)
            {
                return;
            }

            if(this.HasTilesFalling)
            {
                return;
            }

            this.grabbedTile = tileView;
        }

        private bool HasTilesFalling => this.fallingTiles.Count > 0;

        public void Moved(ITileView callingTileView, int x, int y)
        {
            if(this.grabbedTile != callingTileView)
            {
                return;
            }

            if(this.HasTilesFalling)
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
                this.UndoPendingMove();
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

            if(this.HasTilesFalling)
            {
                return;
            }

            if(this.pendingMove == null)
            {
                this.grabbedTile = null;
                return;
            }

            this.board.Move(this.pendingMove.FromX, this.pendingMove.FromY, this.pendingMove.ToX, this.pendingMove.ToY);

            this.board.CheckMatches();

            if(!this.board.HasMatches)
            {
                this.UndoPendingMove();
                this.grabbedTile = null;
                return;
            }

            this.HandleMatches();

            this.scoring.MoveMade();
            this.scoreView.UpdateMoves(-1, this.scoring.MovesAllowed - this.scoring.MovesMade);

            this.grabbedTile = null;
            this.pendingMove = null;
        }

        public void StoppedFalling(ITileView callingTileView)
        {
            this.fallingTiles.Remove(callingTileView);

            if(this.HasTilesFalling)
            {
                return;
            }

            this.board.CheckMatches();

            this.HandleMatches();
        }

        private void HandleMatches()
        {
            if(!this.board.HasMatches)
            {
                return;
            }

            var scoreToAdd = 0;

            foreach(var match in this.board.Matches)
            {
                foreach(var matchedCell in match.MatchedCells)
                {
                    this.tiles[matchedCell.X, matchedCell.Y].Destroy(this.tileMatchValue);
                }
                scoreToAdd = scoreToAdd + match.Length * this.tileMatchValue;
            }

            this.scoring.AddScore(scoreToAdd);
            this.scoring.AddMatches(this.board.Matches.Count);
            this.scoreView.UpdateScore(scoreToAdd, this.scoring.CurrentScore);
            this.scoreView.UpdateMatches(this.board.Matches.Count, this.scoring.CurrentMatches);

            this.board.ClearMatches();

            var falls = this.board.FallTiles();

            foreach(var fall in falls)
            {
                var tileViewToFall = this.tiles[fall.From.X, fall.From.Y];
                this.tiles[fall.To.X, fall.To.Y] = tileViewToFall;
                tileViewToFall.Fall(fall.To.X, fall.To.Y);
            }

            var spawns = this.board.FillTiles(this.tileGenerator);

            foreach(var spawn in spawns)
            {
                this.tiles[spawn.X, spawn.Y] = this.tileViewFactory.CreateInitial(this, spawn.Tile.Type, spawn.X, spawn.Y);
                this.tiles[spawn.X, spawn.Y].Fall(spawn.X, spawn.Y);
                this.fallingTiles.Add(this.tiles[spawn.X, spawn.Y]);
            }
        }

        private void UndoPendingMove()
        {
            var tileToUndo = this.tiles[this.pendingMove.FromX, this.pendingMove.FromY];
            tileToUndo.Move(this.pendingMove.ToX, this.pendingMove.ToY);
            this.tiles[this.pendingMove.ToX, this.pendingMove.ToY] = tileToUndo;
            this.grabbedTile.Move(this.pendingMove.FromX, this.pendingMove.FromY);
            this.tiles[this.pendingMove.FromX, this.pendingMove.FromY] = this.grabbedTile;
            this.pendingMove = null;
        }
    }
}
