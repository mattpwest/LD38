namespace Match3.Core.UI.Views
{
    public interface IScoreView
    {
        void Add(int score, int matchCount);

        void AddScore(int score);

        void AddMatches(int matchCount);
    }
}
