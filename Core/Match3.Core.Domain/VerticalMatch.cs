using System;

namespace Match3.Core.Domain
{
    public class VerticalMatch : Match
    {
        public int StartY { get; private set; }
        public int EndY { get; private set; }

        public VerticalMatch(Cell cell)
            : base(cell)
        {
            cell.MarkVerticalClean();
            this.StartY = this.seedCell.Y;
            this.EndY = this.seedCell.Y;
        }

        protected override bool CheckInMatch(Cell cellToMatch)
        {
            if (cellToMatch.IsEmpty)
            {
                cellToMatch.MarkVerticalClean();
                return false;
            }

            if (cellToMatch.Tile != this.seedCell.Tile)
            {
                return false;
            }

            if(cellToMatch.X != this.seedCell.X)
            {
                return false;
            }

            if(cellToMatch.Y < this.StartY - 1 || this.EndY + 1 < cellToMatch.Y)
            {
                return false;
            }

            if(this.StartY <= cellToMatch.Y && cellToMatch.Y <= this.EndY)
            {
                throw new InvalidOperationException("This cell has already been matched");
            }

            if(cellToMatch.Y < this.StartY)
            {
                this.StartY = cellToMatch.Y;
            }

            if(cellToMatch.Y > this.EndY)
            {
                this.EndY = cellToMatch.Y;
            }

            cellToMatch.MarkVerticalClean();
            return true;
        }
    }
}
