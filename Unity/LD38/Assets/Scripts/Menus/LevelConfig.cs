using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelConfig : MonoBehaviour
{
    public Sprite done;
    public Sprite next;
    public int seed;
    public int width;
    public int height;
    public int scoreGoal;
    public int matchGoal;
    public int moveGoal;
    public string location;
    public string description;
    public string goals;

    private Transform briefingPanel;
    private LevelController levelController;
    private Image image;
    private bool mustUpdateUI = false;
    private bool show = false;

	void Start ()
	{
	    this.briefingPanel = gameObject.transform.parent.parent.Find("BriefingPanel");
	    this.levelController = gameObject.transform.parent.parent.GetComponent<LevelController>();
	    this.image = GetComponent<Image>();
	}
	
	void Update () {
	    if(this.show && !this.image.enabled)
	    {
	        this.image.enabled = true;
	    } else if(!this.show && this.image.enabled)
	    {
	        this.image.enabled = false;
	    }

	    if(this.mustUpdateUI)
	    {
	        this.briefingPanel.Find("LocationText").GetComponent<Text>().text = this.location.Replace('~', '\n');
	        this.briefingPanel.Find("GoalsText").GetComponent<Text>().text = this.goals.Replace('~', '\n');
	        this.briefingPanel.Find("DescriptionText").GetComponent<Text>().text = this.description;
	        this.levelController.CurrentConfig = this;
	        this.mustUpdateUI = false;
	    }
	}

    public void OnLevelItemClicked()
    {
        this.mustUpdateUI = true;
    }

    public void Hide()
    {
        this.show = false;
    }

    public void Show()
    {
        this.show = true;
    }

    public void ShowAsDone()
    {
        this.image.sprite = this.done;
        this.show = true;
    }

    public void ShowAsNext()
    {
        this.image.sprite = this.next;
        this.show = true;
    }
}
