using System;
using System.Collections.Generic;
using System.Linq;

namespace Match3.Core.Domain
{
    public class Board
    {
        private readonly int matchLength;
        private readonly Cell[,] cells;
        private readonly List<Match> matches;
        private readonly IDictionary<int, Cell> previousBottomMatches;
        private readonly IDictionary<int, Cell> bottomEmptyCells;

        public int Width => this.cells.GetLength(0);
        public int Height => this.cells.GetLength(1);
        public IList<Match> Matches => this.matches;

        private Board()
        {
            this.matchLength = 3;
            this.matches = new List<Match>();
            this.previousBottomMatches = new Dictionary<int, Cell>();
            this.bottomEmptyCells = new Dictionary<int, Cell>();
        }

        internal Board(int width, int height)
            : this()
        {
            this.cells = new Cell[width, height];

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    this.cells[x, y] = new Cell(x, y);
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return this.cells[x, y].Tile;
        }

        public void CheckMatches()
        {
            for(int x = 0; x < this.Width; x++)
            {
                for(int y = 0; y < this.Height; y++)
                {
                    var cellToCheck = this.cells[x, y];

                    if(!cellToCheck.IsDirty || cellToCheck.IsEmpty)
                    {
                        continue;
                    }

                    if(cellToCheck.IsHorizontalDirty)
                    {
                        var horizontalMatch = new HorizontalMatch(cellToCheck);
                        for(var xToFind = x - 1; xToFind >= 0; xToFind--)
                        {
                            var cell = this.cells[xToFind, y];

                            var match = horizontalMatch.MatchTo(cell);

                            if(match)
                            {
                                continue;
                            }
                            break;
                        }
                        for(var xToFind = x + 1; xToFind < this.Width; xToFind++)
                        {
                            var cell = this.cells[xToFind, y];
                            var match = horizontalMatch.MatchTo(cell);

                            if(match)
                            {
                                continue;
                            }
                            break;
                        }

                        if(horizontalMatch.Length >= 3)
                        {
                            this.matches.Add(horizontalMatch);
                        }
                    }

                    if(cellToCheck.IsVerticalDirty)
                    {
                        var verticalMatch = new VerticalMatch(cellToCheck);
                        for(int yToFind = y - 1; yToFind >= 0; yToFind--)
                        {
                            var cell = this.cells[x, yToFind];
                            var match = verticalMatch.MatchTo(cell);

                            if(match)
                            {
                                continue;
                            }
                            break;
                        }
                        for(int yToFind = y + 1; yToFind < this.Height; yToFind++)
                        {
                            var cell = this.cells[x, yToFind];
                            var match = verticalMatch.MatchTo(cell);

                            if(match)
                            {
                                continue;
                            }
                            break;
                        }

                        if(verticalMatch.Length >= this.matchLength)
                        {
                            this.matches.Add(verticalMatch);
                        }
                    }
                }
            }
        }

        public void ClearMatches()
        {
            foreach(var matchedCell in this.Matches.SelectMany(x => x.MatchedCells))
            {
                matchedCell.Clear();

                if(!this.previousBottomMatches.ContainsKey(matchedCell.X))
                {
                    this.previousBottomMatches.Add(matchedCell.X, matchedCell);
                    continue;
                }

                if(this.previousBottomMatches[matchedCell.X].Y < matchedCell.Y)
                {
                    continue;
                }

                this.previousBottomMatches[matchedCell.X] = matchedCell;
            }

            this.matches.Clear();
        }

        public void Move(int xStart, int yStart, int xEnd, int yEnd)
        {
            if(this.Matches.Any())
            {
                throw new InvalidOperationException("Can't make move until matches have been resolved");
            }

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

            var start = this.cells[xStart, yStart];
            var end = this.cells[xEnd, yEnd];

            if(start.Tile == end.Tile)
            {
                return;
            }

            var temp = start.Tile;
            start.Tile = end.Tile;
            end.Tile = temp;
        }

        public IList<Fall> FallTiles()
        {
            var falls = new List<Fall>();
            foreach(var cell in this.previousBottomMatches.Values)
            {
                int x = cell.X;
                var emptyY = cell.Y;
                for(int y = emptyY + 1; y < this.Height; y++)
                {
                    if(this.cells[x, y].IsEmpty)
                    {
                        continue;
                    }

                    var from = this.cells[x, y];
                    var to = this.cells[x, emptyY];
                    falls.Add(new Fall(from, to));
                    emptyY++;
                }

                if(emptyY >= this.Height)
                {
                    continue;
                }

                this.bottomEmptyCells.Add(x, this.cells[x, emptyY]);
            }

            this.previousBottomMatches.Clear();
            return falls;
        }

        public IList<Cell> FillTiles(ITileGenerator tileGenerator)
        {
            var spawns = new List<Cell>();
            foreach(var cell in this.bottomEmptyCells.Values)
            {
                int x = cell.X;
                for(int y = cell.Y; y < this.Height; y++)
                {
                    var emptyCell = this.cells[x, y];
                    emptyCell.Tile = tileGenerator.GenerateTile();
                    spawns.Add(emptyCell);
                }
            }

            this.bottomEmptyCells.Clear();
            return spawns;
        }

        internal Cell GetCell(int x, int y)
        {
            return this.cells[x, y];
        }
    }
}
