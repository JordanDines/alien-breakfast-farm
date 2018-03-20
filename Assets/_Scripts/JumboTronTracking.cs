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
		Quaternion temp = transform.rotation;
		temp.x = 0;
		temp.z = 0;
		transform.rotation = temp;
	}

}
