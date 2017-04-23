using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    const float KEY_COOLDOWN = 1.0f;

    public int width = 10;
    public int height = 10;
    public Transform game;
    public bool debug = false;

    private TileViewFactory tileViewFactory;
    private float keyCooldown = 1.0f;

    void Start ()
    {
        tileViewFactory = Instantiate(this.game).GetComponent<TileViewFactory>();
        this.tileViewFactory.StartNewGame(Game.Instance.Width, Game.Instance.Height, Game.Instance.Seed);
    }
	
	void Update ()
	{
	    if(!this.debug)
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
            this.tileViewFactory.StartNewGame(this.width, this.height, seed);
            this.keyCooldown = 0.0f;
	    }
        else if(Input.GetKeyDown(KeyCode.S))
	    {
	        Debug.Log("Restarting with new seed...");
	        Destroy(this.tileViewFactory.gameObject);
	        this.tileViewFactory = Instantiate(this.game).GetComponent<TileViewFactory>();
            this.tileViewFactory.StartNewGame(this.width, this.height);
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
	        Game.Instance.Level++;
            SceneManager.LoadScene("Menu_Levels", LoadSceneMode.Single);
	    }
	}
}
