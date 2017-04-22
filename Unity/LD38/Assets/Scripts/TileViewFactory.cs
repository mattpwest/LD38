using Match3.Core.UI.Presenters;
using Match3.Core.UI.Views;
using UnityEngine;

public class TileViewFactory : MonoBehaviour, ITileViewFactory
{
    const int LMB = 0;
    readonly Vector2 VECTOR2_NONE = new Vector2(-10000, -10000);

    public Transform TileView;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

	void Start ()
	{
	    var bounds = Camera.main.OrthographicBounds();
	    this.minX = -bounds.max.x;
	    this.minY = -bounds.max.y;
        this.maxX = bounds.max.x;
	    this.maxY = bounds.max.y;

        Debug.Log("MinX: " + minX);
	    Debug.Log("MinY: " + minY);
	    Debug.Log("MaxX: " + maxX);
	    Debug.Log("MaxY: " + maxY);

        // Test: for now
	    StubBoardPresenter boardPresenter = GetComponent<StubBoardPresenter>();
	    CreateInitial(boardPresenter, "Test", 0, 0);
	    CreateInitial(boardPresenter, "Test", 0, 1);
	    CreateInitial(boardPresenter, "Test", 0, 2);

	    Create(boardPresenter, "Test", 1, 0);
	    Create(boardPresenter, "Test", 1, 1);
	    Create(boardPresenter, "Test", 1, 2);
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
        var viewTransform = Instantiate(this.TileView, new Vector3(startX, startY, 0.0f), Quaternion.identity);
        var tileView = viewTransform.GetComponent<TileView>();
        tileView.X = x;
        tileView.Y = y;

        // TODO: Use Type...

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
