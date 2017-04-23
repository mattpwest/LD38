using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Transform[] Levels;
    public LevelConfig CurrentConfig { get; set; }
    private bool Started = false;

	void Start ()
	{
	    if(Game.Instance.Level < this.Levels.Length)
	    {
	        this.Levels[Game.Instance.Level].GetComponent<LevelConfig>().OnLevelItemClicked();
	    }
	    else
	    {
	        this.Levels[this.Levels.Length - 1].GetComponent<LevelConfig>().OnLevelItemClicked();
        }
	}
	
	void Update () {
	    if(this.Started)
	    {
	        return;
	    }

	    for(int i = 0; i < this.Levels.Length; i++)
	    {
	        if(i < Game.Instance.Level)
	        {
	            this.Levels[i].GetComponent<LevelConfig>().ShowAsDone();
	        }
            else if(i == Game.Instance.Level)
	        {
	            this.Levels[i].GetComponent<LevelConfig>().ShowAsNext();
	        }
	    }

	    this.Started = true;
	}

    public void StartGame()
    {
        Game.Instance.Seed = CurrentConfig.seed;
        Game.Instance.Width = CurrentConfig.width;
        Game.Instance.Height = CurrentConfig.height;

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
