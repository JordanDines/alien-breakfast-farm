using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public bool canHold = true;
	public bool canPlace = false;

	public GameObject holdingObject;

	public GameObject plateUp;

	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	void Update () {
		if (holdingObject != null && holdingObject.GetComponent<ObjectInteract> ().isReady) {
			plateUp.GetComponent<Collider> ().enabled = true;
		} else {
			plateUp.GetComponent<Collider> ().enabled = false;
		}
	}


}
