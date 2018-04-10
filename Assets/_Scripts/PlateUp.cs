﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateUp : MonoBehaviour {
	[HideInInspector]
	public ObjectInteract oi;
	private GameManager gm;

	[HideInInspector]
	public ObjectInteract heldObject;
	[Tooltip("Reference the space the Ingredient will go to")]
	public GameObject placePoint;
	[Space (10)]
	[Tooltip("The speed at which the ingredient goes to the Place Point")]
	public float grabbingSpeed = 1;
	[HideInInspector]
	public bool isPlating = false;

	public AudioSource putDownSound;

	public GameObject plateUpGlow;

	public List<GameObject> listOfIngredients = new List<GameObject>();

	//public List<Ingredient> currentlyPlated = new List<Ingredient>();

	// Use this for initialization
	void Start () {
		//Find the GameManager in the scene to reference later on
		gm = FindObjectOfType<GameManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		if(isPlating) 
		{
			MoveTowardsPlateUp ();
		}

		CheckPlateInteractivity ();

	}


	void MoveTowardsPlateUp () {
		Vector3 tempOffset = gm.holdingObject.GetComponent<ObjectInteract> ().offset;

		if (gm.previousObject == null) {
			gm.holdingObject.transform.position = Vector3.Lerp
				(gm.holdingObject.transform.position, placePoint.transform.position, grabbingSpeed * Time.deltaTime);

			//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)
			gm.holdingObject.transform.rotation = Quaternion.Euler (new Vector3 (tempOffset.x, tempOffset.y, tempOffset.z));
			//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
			if (Vector3.Distance (gm.holdingObject.transform.position, placePoint.transform.position) < .007f) {
				isPlating = false;
				gm.canHold = true;
				gm.canPlace = false;
				gm.holdingObject.GetComponent<ObjectInteract> ().interactable = false;
				gm.previousObject = gm.holdingObject;
				gm.holdingObject = null;
			}


		} else {
			GameObject tempPlace = gm.previousObject.GetComponent<ObjectInteract> ().staplePoint;
			//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)
			gm.holdingObject.transform.rotation = Quaternion.Euler (new Vector3(tempOffset.x, tempOffset.y, tempOffset.z));
			gm.holdingObject.transform.position = Vector3.Lerp
				(gm.holdingObject.transform.position, tempPlace.transform.position, grabbingSpeed * Time.deltaTime);
			//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
			if (Vector3.Distance (gm.holdingObject.transform.position, tempPlace.transform.position) < .007f) {
				isPlating = false;
				gm.canHold = true;
				gm.canPlace = false;
				gm.holdingObject.GetComponent<ObjectInteract> ().interactable = false;
				gm.previousObject = gm.holdingObject;
				gm.holdingObject = null;
			}
		}
	}


	public void PlaceFood () {
		if (gm.holdingObject.GetComponent<ObjectInteract> ().isReady || gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.needsToBeCooked == false) {
			gm.holdingObject.GetComponent<ObjectInteract> ().respawnParent.GetComponent<Respawn> ().RespawnIngredient ();
			gm.holdingObject.transform.SetParent (placePoint.transform);
			oi = gm.holdingObject.GetComponent<ObjectInteract> ();
			gm.currentlyPlated.Add (oi.ingredient);
			isPlating = true;
			putDownSound.Play ();

		} else if (!gm.holdingObject.GetComponent<ObjectInteract> ().isReady && gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.needsToBeCooked) {
			Debug.Log ("Sorry, this food is not yet ready. Try cooking it, idiot");
			return;
		}
	}
	void CheckPlateInteractivity ()
	{
		if (isPlating) {
			//Make it not interactable
			plateUpGlow.SetActive(false);
			GetComponent<Collider> ().enabled = false;
		}
		//Checks if the currently held object is ready to be plated up
		else if (gm.holdingObject != null && (gm.holdingObject.GetComponent<ObjectInteract> ().isReady == true || gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.needsToBeCooked == false)) {
			//Make the plate up area interactable
			plateUpGlow.SetActive(true);
			GetComponent<Collider> ().enabled = true;
		}
	}

}
