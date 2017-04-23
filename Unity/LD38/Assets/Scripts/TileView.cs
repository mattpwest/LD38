using System;
using Match3.Core.UI.Presenters;
using Match3.Core.UI.Views;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileView : MonoBehaviour, ITileView, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    const int TILE_SIZE = 32;

    public int X { get; set; }
    public int Y { get; set; }
    public int OriginalY { get; private set; }
    public int OriginalX { get; private set; }
    public IBoardPresenter Presenter { get; set; }

    private Vector2 targetPosition;
    private float minX;
    private float minY;
    private float baseX; // Starting X position as calculated by the tile factory

    private Vector2 dragStart;
    private bool falling;

    void Start()
    {
        var bounds = Camera.main.OrthographicBounds();
        this.minX = -bounds.max.x;
        this.minY = -bounds.max.y;
        this.baseX = transform.position.x - X * 1.0f;
    }

    void Update()
    {
        this.targetPosition = new Vector2(this.baseX + X * 1.0f, this.minY + 0.5f + Y * 1.0f);

        Vector2 currentPosition = this.transform.position;

        var distanceFromTarge = this.targetPosition - currentPosition;
        if(!(Math.Abs(distanceFromTarge.sqrMagnitude) < 0.0005f))
        {

            this.transform.position = currentPosition + (this.targetPosition - currentPosition) * 0.1f;
            return;
        }

        if(!this.falling)
        {
            return;
        }

        this.falling = false;
        this.Presenter.StoppedFalling(this);
    }

    public void Fall(int x, int y)
    {
        this.X = x;
        this.Y = y;
        this.falling = true;
    }

    public void Move(int x, int y)
    {
        this.X = x;
        this.Y = y;
        this.baseX = this.baseX - (this.OriginalX - this.X);
    }

    public void Destroy(int tileMatchValue)
    {
        Destroy(gameObject);
        // TODO: Add destroy FX
    }

    public void OnDrag(PointerEventData eventData)
    {
        const float moveThreshold = 0.25f * TILE_SIZE;

        var deltaX = eventData.position.x - this.dragStart.x;
        var deltaY = eventData.position.y - this.dragStart.y;
        var xOverThreshold = Mathf.Abs(deltaX) > moveThreshold;
        var yOverThreshold = Mathf.Abs(deltaY) > moveThreshold;

        if(xOverThreshold && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX < 0) {
                this.Presenter.Moved(this, this.OriginalX - 1, this.OriginalY);
                //Debug.Log("Moved left");
            } else if (deltaX > 0) {
                this.Presenter.Moved(this, this.OriginalX + 1, this.OriginalY);
                //Debug.Log("Moved right");
            }
        }
        else if(yOverThreshold && Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
        {
            if (deltaY < 0) {
                this.Presenter.Moved(this, this.OriginalX, this.OriginalY - 1);
                //Debug.Log("Moved down");
            } else if (deltaY > 0) {
                this.Presenter.Moved(this, this.OriginalX, this.OriginalY + 1);
                //Debug.Log("Moved up");
            }
        }
        else
        {
            //Debug.Log("Moved center");
            this.Presenter.Moved(this, this.OriginalX, this.OriginalY);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.OriginalX = X;
        this.OriginalY = Y;

        this.dragStart = eventData.position;
        this.Presenter.Grabbed(this);
        //Debug.Log("Grabbed");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.Presenter.Released(this);
        //Debug.Log("Released");
    }
}