using System.Collections;
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

	//public List<Ingredient> currentlyPlated = new List<Ingredient>();

	// Use this for initialization
	void Start () {
		//Find the GameManager in the scene to reference later on
		gm = FindObjectOfType<GameManager> ();

	}
	
	// Update is called once per frame
	void Update () {
//		if (gm.holdingObject != null && heldObject.isReady){
//			transform.GetComponent<Collider> ().enabled = true;
//		}
//		else {
//			transform.GetComponent<Collider> ().enabled = false;
//		}
		if(isPlating) 
		{
			MoveTowardsPlateUp ();
		}
		//if (Input.GetKeyDown(KeyCode.A)) {
		//	
		//	for (int i = 0; i < gm.currentRecipe.ingredientsInRecipe.Count; i++) {
		//		currentlyPlated.Add ();
		//	}
		//}
		//if(gm.currentRecipe.ingredientsInRecipe.Contains()) {
		//
		//}

	}


	void MoveTowardsPlateUp () {
		//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)
		gm.holdingObject.transform.rotation = placePoint.transform.rotation;
		gm.holdingObject.transform.position = Vector3.Lerp
			(gm.holdingObject.transform.position, placePoint.transform.position, grabbingSpeed);
		//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
		if (Vector3.Distance (gm.holdingObject.transform.position, placePoint.transform.position) < .1f) {
			isPlating = false;
			gm.holdingObject.GetComponent<ObjectInteract> ().interactable = false;
			gm.canHold = true;
			gm.canPlace = false;
			gm.holdingObject = null;
		}

	}


	public void PlaceFood () {
		if (gm.holdingObject.GetComponent<ObjectInteract> ().isReady || gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.needsToBeCooked == false) {
			oi = gm.holdingObject.GetComponent<ObjectInteract> ();
			gm.currentlyPlated.Add (oi.ingredient);
			isPlating = true;
			//button.transform.GetComponent<Collider> ().enabled = true;
			//button.transform.GetComponent <Appliances> ().tempHeldObj = heldObject;
			//isPlacing = true;
			gm.holdingObject.transform.SetParent (placePoint.transform.parent);
			transform.GetComponent<Collider> ().enabled = false;
		} else if (!gm.holdingObject.GetComponent<ObjectInteract> ().isReady && gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.needsToBeCooked) {
			Debug.Log ("Sorry, this food is not yet ready. Try cooking it, idiot");
			return;
		}
	}

}
