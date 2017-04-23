using Match3.Core.UI.Views;
using UnityEngine;

public class ConsoleScore : IScoreView
{
    private int score;
    private int matchCount;

    public static IScoreView NewInstance
    {
        get
        {
            return new ConsoleScore();
        }
    }

    private ConsoleScore()
    {
        this.score = 0;
        this.matchCount = 0;

        this.LogScoreAndMatches();
    }

    public void Add(int score, int matchCount)
    {
        this.AddScoreWithoutLog(score);
        this.AddMatchesWithoutLog(matchCount);

        this.LogScoreAndMatches();
    }

    public void AddScore(int score)
    {
        this.AddScoreWithoutLog(score);
        this.LogScoreAndMatches();
    }

    public void AddMatches(int matchCount)
    {
        this.AddMatchesWithoutLog(matchCount);
        this.LogScoreAndMatches();
    }

    private void AddScoreWithoutLog(int score)
    {
        if(score == 0)
        {
            return;
        }
        this.score = this.score + score;
    }

    private void AddMatchesWithoutLog(int matchCount)
    {
        if(matchCount == 0)
        {
            return;
        }
        this.matchCount = this.matchCount + matchCount;
    }

    private void LogScoreAndMatches()
    {
        var message = string.Format("Score: {0}\nMatches {1}", this.score, this.matchCount);
        Debug.Log(message);
    }
}
