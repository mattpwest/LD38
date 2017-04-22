using System.Collections;
using System.Collections.Generic;
using Match3.Core.UI.Views;
using UnityEngine;

public class TileView : MonoBehaviour, ITileView {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

    public int X { get; private set; }
    public int Y { get; private set; }
}
