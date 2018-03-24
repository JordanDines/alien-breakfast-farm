using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//If the player can hold something
	public bool canHold = true;
	//If the player is holding something and can place it
	public bool canPlace = false;

	//Reference to the currently held object
	public GameObject holdingObject;

	//Reference to the plate up area
	public GameObject plateUp;

	void Start () {
		//Locks the rotation of the screen so the home button is on the right
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	void Update () {
		//Checks if the currently held object is ready to be plated up
		if (holdingObject != null && holdingObject.GetComponent<ObjectInteract> ().isReady) {
			//Make the plate up area interactable
			plateUp.GetComponent<Collider> ().enabled = true;
		} else {
			//Make it not interactable
			plateUp.GetComponent<Collider> ().enabled = false;
		}
	}
}
