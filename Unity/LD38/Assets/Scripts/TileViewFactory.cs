using Match3.Core.UI.Presenters;
using Match3.Core.UI.Views;
using UnityEngine;
using System;

public class TileViewFactory : MonoBehaviour, ITileViewFactory
{
    const int LMB = 0;
    readonly Vector2 VECTOR2_NONE = new Vector2(-10000, -10000);

    public Transform[] TileViews;
    public string[] TileViewNames;

    public int Seed
    {
        get
        {
            return this.newSeed;
        }
    }

    private float minX;
    private float maxY;

    private Transform tiles;
    private ScoreView scoreView;
    private EndGameView endGameView;

    private BoardPresenter currentGame;
    private int newWidth = -1;
    private int newHeight = -1;
    private int newSeed = -1;
    private int newScoreGoal = -1;
    private int newMatchGoal = -1;
    private int newMovesGoal = -1;

    void Start ()
	{
	    var bounds = Camera.main.OrthographicBounds();
	    this.minX = -bounds.max.x;
	    this.maxY = bounds.max.y;

	    this.tiles = gameObject.transform.Find("Tiles");
	}
	
	void Update ()
	{
	    if(this.newWidth > 0 && this.currentGame == null)
	    {
	        this.scoreView = GameObject.FindWithTag("ScoreView").GetComponent<ScoreView>();
	        this.endGameView = GameObject.FindWithTag("EndGameView").GetComponent<EndGameView>();
            var random = RNG.NewInstance(newSeed);
	        Debug.Log(string.Format("Starting game with seed: {0}", newSeed));
	        this.currentGame = new BoardPresenter(this.scoreView, this.endGameView,
                                                    this, random, this.newWidth, this.newHeight,
	                                                this.newScoreGoal, this.newMatchGoal,
                                                    this.newMovesGoal, this.TileViewNames);
        }
	}

    public void StartNewGame(int width, int height, int scoreGoal, int matchGoal, int movesGoal)
    {
        var random = RNG.NewInstance();
        this.StartNewGame(width, height, random.Seed, scoreGoal, matchGoal, movesGoal);
    }

    public void StartNewGame(int width, int height, int seed, int scoreGoal, int matchGoal, int movesGoal)
    {
        this.newWidth = width;
        this.newHeight = height;
        this.newSeed = seed;
        this.newScoreGoal = scoreGoal;
        this.newMatchGoal = matchGoal;
        this.newMovesGoal = movesGoal;
        this.currentGame = null;
    }

    public ITileView CreateInitial(IBoardPresenter presenter, string type, int x, int y)
    {
        var startX = -this.newWidth * 0.5f + x * 1.0f;
        var startY = this.maxY + 1.5f + y * 1.0f;

        return CreateAtLocation(presenter, type, x, y, startX, startY);
    }

    public ITileView Create(IBoardPresenter presenter, string type, int x, int y)
    {
        var startX = -this.newWidth * 0.5f + x * 1.0f;
        var startY = this.maxY + 1.5f;

        return CreateAtLocation(presenter, type, x, y, startX, startY);
    }

    private ITileView CreateAtLocation(IBoardPresenter presenter, string type, int x, int y, float startX, float startY)
    {
        var index = Array.IndexOf(this.TileViewNames, type);
        if(index < 0 || index >= this.TileViews.Length)
        {
            throw new IndexOutOfRangeException("no TileView found for type " + type);
        }

        var viewTransform = Instantiate(this.TileViews[index], new Vector3(startX, startY, 0.0f), Quaternion.identity, this.tiles);
        var tileView = viewTransform.GetComponent<TileView>();
        tileView.Presenter = presenter;

        return tileView;
    }

    private Vector2 GetDownPosition()
    {
        if(Input.touchSupported)
        {
            if (Input.touchCount == 0) {
                return this.VECTOR2_NONE;
            }

            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                return Input.GetTouch(0).position;
            }
        } 
        else if(Input.GetMouseButtonDown(LMB))
        {
            return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        return this.VECTOR2_NONE;
    }
}
