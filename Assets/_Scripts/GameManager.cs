using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public bool canHold = true;
	public bool canPlace = false;

	public GameObject holdingObject;

	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}


}
