using System.Collections;
using System.Collections.Generic;
using Match3.Core.UI.Presenters;
using Match3.Core.UI.Views;
using UnityEngine;

public class StubBoardPresenter : MonoBehaviour, IBoardPresenter {

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void Grabbed(ITileView tileView)
    {
        throw new System.NotImplementedException();
    }

    public void Moved(ITileView callingTileView, int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public void Released(ITileView callingTileView)
    {
        throw new System.NotImplementedException();
    }
}
