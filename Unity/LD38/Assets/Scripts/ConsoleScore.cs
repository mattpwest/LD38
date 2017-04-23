using Match3.Core.UI.Views;
using UnityEngine;

public class ConsoleScore : IScoreView
{
    public static IScoreView NewInstance
    {
        get
        {
            return new ConsoleScore();
        }
    }

    private ConsoleScore()
    {}

    public void UpdateScore(int scoreDiff, int newScore)
    {
        var message = string.Format("Score: {0} ({1})", newScore, scoreDiff);
        Debug.Log(message);
    }

    public void UpdateMatches(int matchesDiff, int matches)
    {
        var message = string.Format("Matches: {0} ({1})", matches, matchesDiff);
        Debug.Log(message);
    }

    public void UpdateMoves(int movesDiff, int moves)
    {
        var message = string.Format("Moves left: {0} ({1})", moves, movesDiff);
        Debug.Log(message);
    }

    public void SetScore(int score)
    {
        var message = string.Format("Score: {0}", score);
        Debug.Log(message);
    }

    public void SetMatches(int matches)
    {
        var message = string.Format("Matches: {0}", matches);
        Debug.Log(message);
    }

    public void SetMoves(int moves)
    {
        var message = string.Format("Moves left: {0}", moves);
        Debug.Log(message);
    }
}
