using System;

namespace Match3.Core.Domain
{
    public class HorizontalMatch : Match
    {
        public int StartX { get; private set; }
        public int EndX { get; private set; }

        public HorizontalMatch(Cell cell)
            : base(cell)
        {
            cell.MarkHorizontalClean();
            this.StartX = this.seedCell.X;
            this.EndX = this.seedCell.X;
        }

        protected override bool CheckInMatch(Cell cellToMatch)
        {
            if (cellToMatch.IsEmpty)
            {
                cellToMatch.MarkHorizontalClean();
                return false;
            }

            if (cellToMatch.Tile != this.seedCell.Tile)
            {
                return false;
            }


            if(cellToMatch.Y != this.seedCell.Y)
            {
                return false;
            }

            if(cellToMatch.X < this.StartX - 1 || this.EndX + 1 < cellToMatch.X)
            {
                return false;
            }

            if(this.StartX <= cellToMatch.X && cellToMatch.X <= this.EndX)
            {
                throw new InvalidOperationException("This cell has already been matched");
            }

            if(cellToMatch.X < this.StartX)
            {
                this.StartX = cellToMatch.X;
            }

            if(cellToMatch.X > this.EndX)
            {
                this.EndX = cellToMatch.X;
            }

            cellToMatch.MarkHorizontalClean();
            return true;
        }
    }
}
