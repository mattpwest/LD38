namespace Match3.Core.UI.Views
{
    public interface IScoreView
    {
        void UpdateScore(int scoreDiff, int score);

        void UpdateMatches(int matchesDiff, int matches);

        void UpdateMoves(int movesDiff, int moves);

        void SetScore(int score);

        void SetMatches(int matches);

        void SetMoves(int moves);
    }
}
