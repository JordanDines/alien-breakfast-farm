using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumboTronTracking : MonoBehaviour 
{
	public float rotateSpeed;
	private Vector3 targetRotation;

	public GameObject player;


	void Start ()
	{
	
	}

	// Update is called once per frame
	void Update () 
	{
		transform.rotation = Quaternion.Lerp (transform.rotation, player.transform.rotation, rotateSpeed);
	}

}
