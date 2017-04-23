namespace Match3.Core.UI.Views
{
    public interface IEndgamView
    {
        void GameLost(int score);

        void GameWon(int score);
    }
}
