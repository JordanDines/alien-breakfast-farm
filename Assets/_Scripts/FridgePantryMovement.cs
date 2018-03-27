using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgePantryMovement : MonoBehaviour 
{
	public bool toggle = false;
	public bool up = false;

	public float speed;
	public Transform posUp;
	public Transform posDown;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (toggle == true) 
		{
			if (up == true) 
			{
				up = false;
				toggle = false;
			} else 
			{
				up = true;
				toggle = false;
			} 
		}
		if (up == true) 
		{ //Move up
			transform.position = Vector3.Lerp (transform.position, posUp.transform.position, speed);
		} else
		{ //Move down
			transform.position = Vector3.Lerp (transform.position, posDown.transform.position, speed);
		}
	}
}
