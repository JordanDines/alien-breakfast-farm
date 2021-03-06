﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliances : MonoBehaviour {
	[HideInInspector]
	public ObjectInteract oi;
	[HideInInspector]
	private GameManager gm;
	[HideInInspector]
	private float grabbingSpeed;

	[HideInInspector]
	//Bools are great
	private bool isPlacing = false;
	[HideInInspector]
	//Bools are great
	public bool inserting = false;
	[HideInInspector]
	//Bools are great
	public bool removing = false;
	[HideInInspector]
	//Bools are great
	public bool isCooking = false;

	[HideInInspector]
	//Timer for the cooking time
	public float cookingTimer;

	[Header("References")]
	//Needs to be the parent object
	[Tooltip("Reference the Appliance Parent GameObject")]
	public GameObject applianceObject;
	//Needs to reference the appliances button
	[Tooltip("Reference this objects Button GameObject")]
	public GameObject button;
	//The holding point the object will go to first
	[Tooltip("Reference the space the Ingredient will go to first")]
	public GameObject holdingPoint1;
	//The second place the object will go to
	[Tooltip("Reference the space the Ingredient will go to second")]
	public GameObject holdingPoint2;
	[HideInInspector]
	//Reference to the current object in the appliance
	public GameObject heldObject;
	[HideInInspector]
	//Secondary reference to the current object in the appliance
	public GameObject tempHeldObj;

	public GameObject cookingParticle;

	public GameObject finishedParticle;

	private PlateUp plateup;

	void Start () {
		//Find the GameManager in the scene to reference later on
		gm = FindObjectOfType<GameManager> ();
		//Reference each of the holding points
		heldObject = null;
		plateup = FindObjectOfType<PlateUp> ();
		grabbingSpeed = gm.grabbingSpeed;
	}

	void Update () {
		if (isPlacing) {
			MoveTowardsPlacement ();
		}
		//This function has been depreciated in this script. See ObjectInteract.cs void MoveTowardPlayer(). 
		//if (isGrabbing) {
		//	//MoveTowardsPlayer ();
		//}
		if (inserting) 
		{
			MoveToHoldingPoint2 ();
		}

		if (isCooking) 
		{
			CookFood ();
		}

		if (removing) 
		{
			MoveToHoldingPoint1 ();
		}

		InteractiveCheck ();

	}
		
	void CookFood () {
		//This function just gets the ingredients cooking time and cooks the food for however long you want
		//Turn the particle system on
		cookingParticle.SetActive(true);
		//Starts the cooking timer
		cookingTimer += Time.deltaTime;
		//If it reaches the ingredients time to cook
		if (cookingTimer >= oi.ingredient.timeToCook) {
			CookedParticle ();
			//Set the ingredient to Ready
			oi.isReady = true;
			//Make it interactable again
			oi.interactable = true;
			//See MovingToHoldingPoint2
			removing = true;
			//Stop it from being cooked
			isCooking = false;
			//Reset the timer so it works within a loop
			cookingTimer = 0;
			//Turn the particle system off
			cookingParticle.SetActive(false);
		}
	}

	void CookedParticle () {
		Instantiate (finishedParticle, holdingPoint1.transform.position, finishedParticle.transform.rotation);
	}

	void MoveTowardsPlacement () {
		//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)
		gm.holdingObject.transform.rotation = applianceObject.GetComponent<Appliances>().holdingPoint1.transform.rotation;
		gm.holdingObject.transform.position = Vector3.Lerp 
			(gm.holdingObject.transform.position, applianceObject.GetComponent<Appliances>().holdingPoint1.transform.position, grabbingSpeed * Time.deltaTime);
		//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
		if (Vector3.Distance (gm.holdingObject.transform.position, applianceObject.GetComponent<Appliances>().holdingPoint1.transform.position) < .007f) {
			//Make the held object non interactable as to not fuck with player feedback (reticle openning and closing)
			gm.holdingObject.GetComponent<ObjectInteract> ().interactable = false;
			//Set the player to be able to pick things up
			gm.canHold = true;
			//Set the player to not be able to put things down
			gm.canPlace = false;
			//Set the player's held object to nothing
			gm.holdingObject = null;
			//Stop this function
			isPlacing = false;
			//Make the button active
			button.transform.GetComponent<Collider> ().enabled = true;
		}
	}

	//This function has been depreciated in this script. See ObjectInteract.cs void MoveTowardPlayer(). 
	//void MoveTowardsPlayer () {
	//		//Turn off the rigidbody and collider
	//		gm.holdingObject.GetComponent<Rigidbody> ().useGravity = false;
	//		gm.holdingObject.GetComponent<Collider> ().enabled = true;
	//		//Then move it to the player and make it look at the player
	//		gm.holdingObject.transform.position = Vector3.Lerp (gm.holdingObject.transform.position, holdingPoint.transform.position, grabbingSpeed);
	//		gm.holdingObject.transform.LookAt(GameObject.FindGameObjectWithTag ("Player").transform.position);
	//		//If it has reached the player's holding point, allow it to be put down in a specific spot
	//		if (Vector3.Distance(transform.position, holdingPoint.transform.position) < 0.1f) {
	//			gm.GetComponent<ObjectInteract> ().interactable = false;
	//		}
	//}

	void MoveToHoldingPoint2 () {
		//Get reference the button's tempHeldObject, then move it to the second holding point over (preset) time. This holding point is the INSERTED transform
		button.GetComponent<Appliances> ().tempHeldObj.transform.position = Vector3.Lerp (tempHeldObj.transform.position, 
			applianceObject.GetComponent<Appliances>().holdingPoint2.transform.position, 
			grabbingSpeed * Time.deltaTime);
		button.transform.GetComponent<Collider> ().enabled = false;
		//If it gets close enough
		if (Vector3.Distance (tempHeldObj.transform.position, holdingPoint2.transform.position) < 0.007f) {
			//button.GetComponent<Appliances> ().tempHeldObj = null;
			//Stop this function from working
			inserting = false;
			//Set the appliance to be interacted with
			//transform.GetComponent<Collider> ().enabled = true;
			//Set the button to non-interactable, as to not interfere with player feedback (reticle size)

		}
		
	}

	void MoveToHoldingPoint1 () {
		applianceObject.GetComponent<Appliances> ().heldObject = null;
		//Get the button's tempHeldObject position, then move it back to the first holding point over (preset) time. This holding point is the REMOVED transform
		button.GetComponent<Appliances> ().tempHeldObj.transform.position = Vector3.Lerp (button.GetComponent<Appliances> ().tempHeldObj.transform.position, 
			applianceObject.GetComponent<Appliances> ().holdingPoint1.transform.position, 
			grabbingSpeed * Time.deltaTime);
		//If it gets close enough
		if (Vector3.Distance (button.GetComponent<Appliances> ().tempHeldObj.transform.position, button.GetComponent<Appliances> ().holdingPoint1.transform.position) < 0.007f) {
			//Stop this function from running
			//GetComponent<Appliances> ().tempHeldObj = null;
			removing = false;
		}
	}

	void InteractiveCheck () {
		//If you are holding something
		if (gm.holdingObject != null) {
			//Check if the thing that you're holding can interact with the thing you're looking at AND if the thing you're looking at isn't already in use
			if (gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.tagsToInteractWith.Contains (transform.tag) 
				&& applianceObject.GetComponent<Appliances> ().heldObject == null 
				&& gm.canPlace == true
				&& plateup.isPlating == false && gm.holdingObject.GetComponent<ObjectInteract>().isReady == false) {
					//Then, make this appliance interactable
					applianceObject.transform.GetComponent<Collider> ().enabled = true;
			} else {
				//Else, make it unusable
				//applianceObject.transform.GetComponent<Collider> ().enabled = false;
			}
			//If you're NOT holding something AND the appliance ISN'T holding something
		} else if (gm.holdingObject == null && applianceObject.GetComponent<Appliances> ().heldObject == null && button.GetComponent<Appliances> ().heldObject == null) {
			
			//Then make the object unsable. This is to prevent player feedback from being confused as to what is usable or not, otherwise a NullReference
			//will come up in the console. Nothing bad will happen just, poor player feedback
			applianceObject.transform.GetComponent<Collider> ().enabled = false;
		} else if (gm.holdingObject == null && applianceObject.GetComponent<Appliances> ().heldObject == null && plateup.isPlating == false) {
			applianceObject.transform.GetComponent<Collider> ().enabled = true;
		}
	}

	#region PLACE_FOOD
	public void PlaceFood () {
		//If the held object has any of the tags liosted in the scriptable object associated with them, then...
		if (gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.tagsToInteractWith.Contains (transform.tag) && plateup.isPlating == false) {
			//Set the parent to the appliances parent
			gm.holdingObject.transform.SetParent (holdingPoint1.transform.parent);
			//See MoveTowardPlacement ()
			isPlacing = true;
			//Set the ObjectInteract script to the one held in the appliance
			oi = gm.holdingObject.GetComponent<ObjectInteract> ();
			//Set the held object the same as the currently held object
			heldObject = gm.holdingObject;
			//Set the button's temp held obj the same as the appliance held object
			button.transform.GetComponent <Appliances> ().tempHeldObj = heldObject;
			//Make the object non-interactable (as to not interfere with the button collider)
			transform.GetComponent<Collider> ().enabled = false;

			//This else if has been depreciated but keeping it here just in case. It just checks if the tags are relevent but I have fixed that above in Update.
		} else if (!gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.tagsToInteractWith.Contains (transform.tag)) {
			Debug.Log ("Sorry, this is tagged as: " + gm.holdingObject.tag + ". This needs to be tagged as: " + gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.tagsToInteractWith.Contains (transform.tag));
			return;
		}
	}

	public void FoodButton () {
		//See MoveToHoldingPoint1 ()
		inserting = true;
		//Make the button non-interactable as to not continuously cook the food
		transform.GetComponent<Collider> ().enabled = false;
		//See CookFood ()
		isCooking = true;
		//Set the reference of the tempHeldObj's ObjectInteract script to the button
		oi = tempHeldObj.GetComponent<ObjectInteract> ();
		} 
		
	#endregion 
}
