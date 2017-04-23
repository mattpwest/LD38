using Match3.Core.UI.Views;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour, IScoreView
{

    private float score = 0;
    private float moves = 0;
    private float matches = 0;

    private float scoreTo = 0;
    private float movesTo = 0;
    private float matchesTo = 0;

    private Text scoreText;
    private Text movesText;
    private Text matchesText;
    private int scoreDiff;

    void Start ()
	{
	    var scoresPanel = gameObject.transform.Find("ScoresPanel");
	    this.scoreText = scoresPanel.Find("ScoreText").GetComponent<Text>();
        this.movesText = scoresPanel.Find("MovesText").GetComponent<Text>();
        this.matchesText = scoresPanel.Find("MatchesText").GetComponent<Text>();
    }
	
	void Update () {
        var scoreDiff = this.scoreTo - this.score;
        if (scoreDiff > 0) {
            this.score += Mathf.Max(1, scoreDiff * Time.deltaTime * 0.6f);
        }

        this.scoreText.text = "" + Mathf.RoundToInt(this.score);
	    this.movesText.text = "" + Mathf.RoundToInt(this.moves);
	    this.matchesText.text = "" + Mathf.RoundToInt(this.matches);
	}

    public void UpdateScore(int scoreDiff, int score)
    {
        this.score = this.scoreTo;
        this.scoreTo = score;
    }

    public void UpdateMatches(int matchesDiff, int matches)
    {
        this.matches = matches;
    }

    public void UpdateMoves(int movesDiff, int moves)
    {
        this.moves = moves;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void SetMatches(int matches)
    {
        this.matches = matches;
    }

    public void SetMoves(int moves)
    {
        this.moves = moves;
    }
}
