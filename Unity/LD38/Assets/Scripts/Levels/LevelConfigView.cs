using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelConfigView : MonoBehaviour
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
    private Sprite currentSprite;
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

	    if(this.image.sprite != this.currentSprite)
	    {
	        this.image.sprite = this.currentSprite;
	    }

	    if(this.mustUpdateUI)
	    {
	        this.briefingPanel.Find("LocationText").GetComponent<Text>().text = this.location.Replace('~', '\n');
	        this.briefingPanel.Find("GoalsText").GetComponent<Text>().text = this.goals.Replace('~', '\n');
	        this.briefingPanel.Find("DescriptionText").GetComponent<Text>().text = this.description;
	        this.levelController.CurrentConfigView = this;
	        this.mustUpdateUI = false;
	    }
	}

    public LevelConfig GetConfig()
    {
        return new LevelConfig(this.seed, this.width, this.height, this.scoreGoal,
            this.matchGoal, this.moveGoal, this.location, this.description, this.goals);
    }

    public void OnLevelItemClicked()
    {
        this.mustUpdateUI = true;

        var allOtherTweens = GameObject.FindObjectsOfType(typeof(SizePulseTween));
        for(int i = 0; i < allOtherTweens.Length; i++)
        {
            ((SizePulseTween) allOtherTweens[i]).Disable();
        }

        if(this.GetComponent<SizePulseTween>() != null)
        {
            this.GetComponent<SizePulseTween>().Enable();
        }
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
        this.currentSprite = this.done;
        this.show = true;
    }

    public void ShowAsNext()
    {
        this.currentSprite = this.next;
        this.show = true;
    }
}
