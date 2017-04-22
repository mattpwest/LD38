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

    public void Moved(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public void Released()
    {
        throw new System.NotImplementedException();
    }
}
