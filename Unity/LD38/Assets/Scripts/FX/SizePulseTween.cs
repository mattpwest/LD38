using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizePulseTween : MonoBehaviour
{

    public float MinSize = 0.8f;
    public float MaxSize = 1.2f;
    public float Duration = 1.0f;

    private bool enabled = true;
    private float position = 0.5f;
    private float direction = 1.0f;
    private float size;

	void Start ()
	{
	    this.size = transform.localScale.x;
	}
	
	void Update ()
	{
	    if(!this.enabled)
	    {
	        return;
	    }

	    this.position += Time.deltaTime * this.direction * this.Duration;
	    if(this.position >= 1.0f || this.position <= 0.0f)
	    {
	        this.direction = -1.0f * this.direction;
	    }

	    var distance = (this.MaxSize - this.MinSize) * this.position;
        this.transform.localScale = new Vector3(this.MinSize + distance, this.MinSize + distance, 1.0f);
	}

    public void Enable()
    {
        this.enabled = true;
    }

    public void Disable()
    {
        this.enabled = false;
        this.position = 0.5f;
        this.direction = 1.0f;
        this.transform.localScale = Vector3.one;
    }
}
