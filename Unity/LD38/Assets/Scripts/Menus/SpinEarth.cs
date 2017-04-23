using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEarth : MonoBehaviour
{

    public float RotationRateInDegreesPerSecond = 10.0f;
    private float yRotation = 0.0f;

	void Start () {
		
	}
	
	void Update ()
	{
	    this.yRotation += Time.deltaTime * this.RotationRateInDegreesPerSecond;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, yRotation, 0.0f));
	}
}
