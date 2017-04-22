using Match3.Core.UI.Views;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TileView : MonoBehaviour, ITileView
{

    private Vector2 targetPosition;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Start() {
        var bounds = Camera.main.OrthographicBounds();
        this.minX = -bounds.max.x;
        this.minY = -bounds.max.y;
        this.maxX = bounds.max.x;
        this.maxY = bounds.max.y;
    }
	
	void Update () {
	    this.targetPosition = new Vector2(this.minX + 0.5f + X * 1.0f, this.minY + 0.5f + Y * 1.0f);

	    Vector2 currentPosition = (Vector2)this.transform.position;
        if (!currentPosition.Equals(this.targetPosition))
	    {
	        this.transform.position = currentPosition + (this.targetPosition - currentPosition) * 0.1f;
	    }
	}

    public void Fall(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public void Move(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public int X { get; set; }
    public int Y { get; set; }

    void OnMouseDown()
    {
        Debug.Log("mouse went down on a tile");
    }
    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("mouse went down on a tile");
    }*/
}
