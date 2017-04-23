using System;

namespace Match3.Core.UI.Presenters
{
    internal class Scoring
    {
        private bool ScoreBeat => this.ScoreRequired == 0 || this.CurrentScore >= this.ScoreRequired;
        private bool MatchesBeat => this.MatchesRequired == 0 || this.CurrentMatches >= this.MatchesRequired;
        private bool MovesAllowedReached => this.MovesMade == this.MovesAllowed;

        public int CurrentScore { get; private set; }
        public int CurrentMatches { get; private set; }
        public int MovesMade { get; private set; }

        public int ScoreRequired { get; }
        public int MatchesRequired { get; }
        public int MovesAllowed { get; }

        public bool HasMovesLeft => this.MovesMade < this.MovesAllowed;
        public bool HasWon => this.ScoreBeat && this.MatchesBeat && this.MovesAllowedReached;
        public bool HasLost => this.MovesAllowedReached && !this.ScoreBeat && !this.MatchesBeat;

        private Scoring()
        {
            this.CurrentScore = 0;
            this.CurrentMatches = 0;
            this.MovesMade = 0;
        }

        public Scoring(int scoreRequired, int matchesRequired, int movesAllowed)
            : this()
        {
            if(scoreRequired < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(scoreRequired), scoreRequired, "Has to be greater than or equal to zero");
            }
            if(matchesRequired < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(matchesRequired), matchesRequired, "Has to be greater than or equal to zero");
            }
            if(movesAllowed <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(movesAllowed), movesAllowed, "Has to be greater than zero");
            }

            this.ScoreRequired = scoreRequired;
            this.MatchesRequired = matchesRequired;
            this.MovesAllowed = movesAllowed;
        }

        public void AddScore(int score)
        {
            this.CurrentScore = this.CurrentScore + score;
        }

        public void AddMatches(int matches)
        {
            this.CurrentMatches = this.CurrentMatches + matches;
        }

        public void MoveMade()
        {
            if(this.MovesAllowedReached)
            {
                throw new InvalidOperationException("Max amount of moves have been made");
            }

            this.MovesMade++;
        }
    }
}
