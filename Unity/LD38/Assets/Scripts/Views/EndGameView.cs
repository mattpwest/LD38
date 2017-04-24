using System.Collections;
using System.Collections.Generic;
using Match3.Core.UI.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameView : MonoBehaviour, IEndgamView {

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void GameLost(int score)
    {
        Game.Instance.AddEvent(new Game.GameLostEvent(Game.Instance.CurrentLevelConfig, score));

        SceneManager.LoadScene("Menu_Levels", LoadSceneMode.Single);
    }

    public void GameWon(int score)
    {
        Game.Instance.AddEvent(new Game.GameWonEvent(Game.Instance.CurrentLevelConfig, score));

        SceneManager.LoadScene("Menu_Levels", LoadSceneMode.Single);
    }
}
