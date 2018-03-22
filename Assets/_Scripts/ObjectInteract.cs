﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour {

	public Ingredient ingredient;

	private GameObject player;
	private GameManager gm;
	[Header("References")]
	[SerializeField]
	private GameObject holdingPoint;

	[Header ("Mesh Changes")]
	public GameObject uncookedMesh;
	public GameObject cookedMesh;

	[Header("Variables")]
	[SerializeField]
	private float grabbingSpeed = 1;
	//private bool isPlacing = false;
	[SerializeField]
	private bool isGrabbing = false;
	[SerializeField]
	public bool hasBeenPlaced = false;
	[SerializeField]
	public bool interactable = true;
	[SerializeField]
	public bool isReady;


	void Start () {
		//Find the GameManager in the scene to reference later on
		gm = FindObjectOfType<GameManager> ();
		//Find the player's HoldingPoint to also reference later on
		holdingPoint = GameObject.FindGameObjectWithTag ("HoldingPoint");
		//Initialize the object currently being held in the GameManager
		gm.holdingObject = null;
		//Set this objects tag to the tag it needs to be
		transform.tag = ingredient.tagThisAs;

	}

	void Update () {
		//These Functions allow the objects to move after the events trigger as buttons are static
		if (isGrabbing) 
		{
			MoveTowardsPlayer ();
		}
		//if (isPlacing) 
		//{
		//	MoveTowardsPlacement ();
		//}
		if (gm.canPlace) 
		{
			HoldingObject ();
		}
		
		if (isReady) 
		{
			FoodReady ();
		}

		if (interactable == true) {
			GetComponent<Collider> ().enabled = true;
		} else if (interactable == false) {
			GetComponent<Collider> ().enabled = false;
		}

	}
	#region BUTTONS

	public void GrabObject () {
		//If you're not holding anything, and you're looking at a valid target to be picked up, pick the object up (See MoveTowardsPlayer())
		if(interactable && gm.canHold && transform.tag == ingredient.tagThisAs) {
			//Turn off the rigidbody and colliderd
			GetComponent<Rigidbody> ().useGravity = false;
			//gameObject.GetComponent<Collider> ().enabled = false;
			isGrabbing = true;
			interactable = false;
			//Set it's parent to your holdingPoint
			transform.SetParent (holdingPoint.transform);
			gm.holdingObject = gameObject;
		}
	}

	public void PlaceObject () {
		//If the food is ready and is looking at the serving area, put the food down (and no one will get hurt)
		if (isReady && gm.canPlace) {

		} 
		//If you're holding something and you're looking at a valid target, put the object down (See MoveTowardsPlacement())
		else if (gm.canPlace && transform.tag == ingredient.tagsToInteractWith.ToString()) {
//			isPlacing = true;
			//Also set it to the specific target gameobject's PlacePoint parent
			gm.holdingObject.transform.SetParent (transform.Find ("PlacePoint").transform);
		}
	}
	#endregion

	#region PLAYER_INTERACTIONS

	void FoodReady () {
		uncookedMesh.SetActive (false);
		cookedMesh.SetActive (true);
	}

	void DropObject () {

	}


	//See Appliances.cs MoveTowardsPlacement()
	//void MoveTowardsPlacement () {
	//	//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)
	//		gm.holdingObject.transform.rotation = transform.Find ("PlacePoint").transform.rotation;
	//		gm.holdingObject.transform.position = Vector3.Lerp (gm.holdingObject.transform.position, transform.Find("PlacePoint").transform.position, grabbingSpeed);
	//		//Also enable the collider to allow it to be interactable again
	//		gm.holdingObject.GetComponent<Collider> ().enabled = true;
	//		//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
	//		if (Vector3.Distance (gm.holdingObject.transform.position, transform.Find("PlacePoint").transform.position) < .1f) {
	//			hasBeenPlaced = true;
	//			isPlacing = false;
	//			gm.canPlace = false;
	//			gm.canHold = true;
	//			gm.holdingObject = null;
	//
	//		}
	//}

	void MoveTowardsPlayer () {
		
		//Then move it to the player and make it look at the player
		transform.position = Vector3.Lerp (transform.position, holdingPoint.transform.position, grabbingSpeed);
		gm.holdingObject.transform.LookAt(GameObject.FindGameObjectWithTag ("Player").transform.position);
		//If it has reached the player's holding point, allow it to be put down in a specific spot
		if (Vector3.Distance(transform.position, holdingPoint.transform.position) < 0.1f) {
			hasBeenPlaced = false;
			gm.canHold = false;
			gm.canPlace = true;
			isGrabbing = false;
		}
	}

	void HoldingObject () {
		//If you're holding the object, make it look at the player. Will follow the player's rotation
		gm.holdingObject.transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform.position);
		gm.holdingObject.transform.rotation = Camera.main.GetComponent<Transform> ().transform.rotation;
	}
	#endregion
		
}
