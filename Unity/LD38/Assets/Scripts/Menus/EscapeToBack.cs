using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeToBack : MonoBehaviour
{
    public string SceneName = "Main";
    public float KeyCooldown = 1.5f;
    private float KeyCooldownPassed = 0.0f;

	void Start () {
		
	}
	
	void Update ()
	{
	    this.KeyCooldownPassed += Time.deltaTime;

	    if(this.KeyCooldownPassed > this.KeyCooldown && Input.GetKeyDown(KeyCode.Escape))
	    {
	        if(this.SceneName.Equals("Exit"))
	        {
	            Application.Quit();
	        }
	        else
	        {
	            SceneManager.LoadScene(this.SceneName, LoadSceneMode.Single);
	        }
	    }
	}
}
