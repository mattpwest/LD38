using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    const float KEY_COOLDOWN = 1.0f;

    public Transform game;
    public bool debug = false;

    private TileViewFactory tileViewFactory;
    private float keyCooldown = 1.0f;
    private LevelConfig config;

    void Start ()
    {
        Game gameInstance = Game.Instance;
        Game.GameEvent lastEvent = gameInstance.LastEvent;

        if(typeof(Game.GameStartedEvent) == lastEvent.GetType())
        {
            config = ((Game.GameStartedEvent)lastEvent).LevelConfig;
            tileViewFactory = Instantiate(this.game).GetComponent<TileViewFactory>();
            this.tileViewFactory.StartNewGame(config.Width, config.Height, config.Seed, config.ScoreGoal, config.MatchGoal, config.MoveGoal);
        }
    }
	
	void Update ()
	{
	    if(!this.debug || this.config == null)
	    {
	        return;
	    }

	    this.keyCooldown += Time.deltaTime;

	    if(Input.GetKeyDown(KeyCode.R) && this.keyCooldown > KEY_COOLDOWN)
	    {
            Debug.Log("Restarting with same seed...");
	        var seed = this.tileViewFactory.Seed;
            Destroy(this.tileViewFactory.gameObject);
	        this.tileViewFactory = Instantiate(this.game).GetComponent<TileViewFactory>();
            this.tileViewFactory.StartNewGame(config.Width, config.Height, seed, config.ScoreGoal, config.MatchGoal, config.MoveGoal);
            this.keyCooldown = 0.0f;
	    }
        else if(Input.GetKeyDown(KeyCode.S))
	    {
	        Debug.Log("Restarting with new seed...");
	        Destroy(this.tileViewFactory.gameObject);
	        this.tileViewFactory = Instantiate(this.game).GetComponent<TileViewFactory>();
            this.tileViewFactory.StartNewGame(config.Width, config.Height, config.ScoreGoal, config.MatchGoal, config.MoveGoal);
            this.keyCooldown = 0.0f;
	    }
        else if(Input.GetKeyDown(KeyCode.B))
	    {
	        Debug.Log("Going back to menu screen...");
            SceneManager.LoadScene("Menu_Levels", LoadSceneMode.Single);
	    }
        else if(Input.GetKeyDown(KeyCode.N))
	    {
	        Debug.Log("Going back to menu screen for next level...");
	        Game.Instance.AddEvent(new Game.GameWonEvent(this.config, 0));
            SceneManager.LoadScene("Menu_Levels", LoadSceneMode.Single);
	    }
	}
}
