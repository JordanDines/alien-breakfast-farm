using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigdePantryButton : MonoBehaviour 
{
	public GameObject objectToMove;

	public void Update ()

	{
		
	}

	public void UpDown()
	{
		objectToMove.GetComponent<FridgePantryMovement> ().toggle = true;
	}
}
