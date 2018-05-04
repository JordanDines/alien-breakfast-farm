using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigdePantryButton : MonoBehaviour 
{
	public GameObject objectToMove;

	private Animator buttonAnimation;

	void Start () {
		buttonAnimation = GetComponent<Animator> ();
	}
		

	public void UpDown()
	{
		objectToMove.GetComponent<FridgePantryMovement> ().toggle = true;
	}

	public void ButtonDown () {
		buttonAnimation.SetBool ("Pressed", true);
	}

	public void ButtonUp () {
		buttonAnimation.SetBool ("Pressed", false);
	}
}
