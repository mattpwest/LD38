using System.Collections.Generic;

namespace Match3.Core.Domain
{
    public abstract class Match
    {
        protected readonly IList<Cell> matchedCells;
        protected readonly Cell seedCell;
        public int Length => this.matchedCells.Count;
        public IEnumerable<Cell> MatchedCells => this.matchedCells;

        private Match()
        {
            this.matchedCells = new List<Cell>();
        }

        protected Match(Cell seedCell)
            : this()
        {
            this.seedCell = seedCell;
            this.matchedCells.Add(this.seedCell);
        }

        public bool MatchTo(Cell cellToMach)
        {
            var match = this.CheckInMatch(cellToMach);

            if(match)
            {
                this.matchedCells.Add(cellToMach);
            }

            return match;
        }

        protected abstract bool CheckInMatch(Cell cellToMatch);
    }
}
