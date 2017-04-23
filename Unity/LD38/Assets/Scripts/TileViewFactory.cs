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
    public int BoardWidth = 5;
    public int BoardHeight = 5;

    private float minX;
    private float maxY;

	void Start ()
	{
	    var bounds = Camera.main.OrthographicBounds();
	    this.minX = -bounds.max.x;
	    this.maxY = bounds.max.y;

        //Debug.Log("MinX: " + minX);
	    //Debug.Log("MinY: " + minY);
	    //Debug.Log("MaxX: " + maxX);
	    //Debug.Log("MaxY: " + maxY);

	    var random = RNG.NewInstance();
        Debug.Log(string.Format("Random Seed: {0}", random.Seed));
	    new BoardPresenter(ConsoleScore.NewInstance, ConsoleEndgame.NewInstance, this, random, this.BoardWidth, this.BoardHeight, this.TileViewNames);
    }
	
	void Update ()
	{
	}

    public ITileView CreateInitial(IBoardPresenter presenter, string type, int x, int y)
    {
        var startX = this.minX + 0.5f + x * 1.0f;
        var startY = this.maxY + 1.5f + y * 1.0f;

        return CreateAtLocation(presenter, type, x, y, startX, startY);
    }

    public ITileView Create(IBoardPresenter presenter, string type, int x, int y)
    {
        var startX = this.minX + 0.5f + x * 1.0f;
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

        var viewTransform = Instantiate(this.TileViews[index], new Vector3(startX, startY, 0.0f), Quaternion.identity);
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
