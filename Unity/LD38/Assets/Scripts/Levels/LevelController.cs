using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Transform[] Levels;
    public LevelConfigView CurrentConfigView { get; set; }
    private bool Started = false;
    private Game gameInstance;

	void Start ()
	{
	    gameInstance = Game.Instance;

	    UpdateCurrentLevelBasedOnEvents();
        MarkPastLevelsAsDone();
	    HideUpcomingLevels();
	}

    private void MarkPastLevelsAsDone() {
        for(int i = 0; i < this.gameInstance.Level; i++)
        {
            this.Levels[i].GetComponent<LevelConfigView>().ShowAsDone();
        }
    }

    private void HideUpcomingLevels() {
        for (int i = this.gameInstance.Level + 1; i < Levels.Length; i++) {
            this.Levels[i].GetComponent<LevelConfigView>().Hide();
        }
    }

    private void UpdateCurrentLevelBasedOnEvents()
    {
        LevelConfigView currentLevel = this.Levels[gameInstance.Level].GetComponent<LevelConfigView>();
        currentLevel.ShowAsNext();

        Game.GameEvent lastEvent = gameInstance.LastEvent;
        if (typeof(Game.GameWonEvent) == lastEvent.GetType()) {
            Game.GameWonEvent winEvent = (Game.GameWonEvent)lastEvent;
            currentLevel.ShowAsDone();

            if(gameInstance.Level < this.Levels.Length - 1)
            {
                gameInstance.Level++;

                currentLevel = this.Levels[gameInstance.Level].GetComponent<LevelConfigView>();
                currentLevel.ShowAsNext();
            }
        } else if (typeof(Game.GameLostEvent) == lastEvent.GetType()) {
            Game.GameLostEvent lostEvent = (Game.GameLostEvent)lastEvent;
        }

        currentLevel.OnLevelItemClicked();
    }

    void Update () {
	}

    public void StartGame()
    {
        this.gameInstance.AddEvent(new Game.GameStartedEvent(this.CurrentConfigView.GetConfig()));

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
