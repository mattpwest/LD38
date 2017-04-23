using Match3.Core.UI.Views;
using UnityEngine;

public class ConsoleEndgame : IEndgamView
{
    public static ConsoleEndgame NewInstance
    {
        get
        {
            return new ConsoleEndgame();
        }
    }

    private ConsoleEndgame() { }

    public void GameLost(int score)
    {
        var message = string.Format("Game lost with {0} points", score);
        Debug.Log(message);
    }

    public void GameWon(int score)
    {
        var message = string.Format("Game won with {0} points", score);
        Debug.Log(message);
    }
}
