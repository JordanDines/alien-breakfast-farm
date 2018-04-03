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

	}


	void MoveTowardsPlateUp () {
		float tempOffsetx = gm.holdingObject.GetComponent<ObjectInteract> ().offset.x;
		float tempOffsety = gm.holdingObject.GetComponent<ObjectInteract> ().offset.y;
		float tempOffsetz = gm.holdingObject.GetComponent<ObjectInteract> ().offset.z;
		if (gm.previousObject == null) {
			//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)
			//gm.holdingObject.transform.rotation = placePoint.transform.rotation;
			gm.holdingObject.transform.rotation = Quaternion.Euler (new Vector3(tempOffsetx,tempOffsety, tempOffsetz));
			//gm.holdingObject.transform.rotation = new Quaternion ((placePoint.transform.rotation.x + tempOffset.x), 
			//	(placePoint.transform.rotation.y + tempOffset.y), (placePoint.transform.rotation.z + tempOffset.z), 0);
			gm.holdingObject.transform.position = Vector3.Lerp
				(gm.holdingObject.transform.position, placePoint.transform.position, grabbingSpeed);
			//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
			if (Vector3.Distance (gm.holdingObject.transform.position, placePoint.transform.position) < .1f) {
				isPlating = false;
				gm.holdingObject.GetComponent<ObjectInteract> ().interactable = false;
				gm.canHold = true;
				gm.canPlace = false;
				gm.previousObject = gm.holdingObject;
				gm.holdingObject = null;
			}


		} else {
			GameObject tempPlace = gm.previousObject.GetComponent<ObjectInteract> ().staplePoint;
			//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)
			gm.holdingObject.transform.rotation = Quaternion.Euler (new Vector3(tempOffsetx, tempOffsety, tempOffsetz));
			gm.holdingObject.transform.position = Vector3.Lerp
				(gm.holdingObject.transform.position, tempPlace.transform.position, grabbingSpeed);
			//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
			if (Vector3.Distance (gm.holdingObject.transform.position, tempPlace.transform.position) < .1f) {
				isPlating = false;
				gm.holdingObject.GetComponent<ObjectInteract> ().interactable = false;
				gm.canHold = true;
				gm.canPlace = false;
				gm.previousObject = gm.holdingObject;
				gm.holdingObject = null;
			}
		}
	}


	public void PlaceFood () {
		if (gm.holdingObject.GetComponent<ObjectInteract> ().isReady || gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.needsToBeCooked == false) {
			oi = gm.holdingObject.GetComponent<ObjectInteract> ();
			gm.currentlyPlated.Add (oi.ingredient);
			isPlating = true;
			gm.holdingObject.transform.SetParent (placePoint.transform);
			transform.GetComponent<Collider> ().enabled = false;

		} else if (!gm.holdingObject.GetComponent<ObjectInteract> ().isReady && gm.holdingObject.GetComponent<ObjectInteract> ().ingredient.needsToBeCooked) {
			Debug.Log ("Sorry, this food is not yet ready. Try cooking it, idiot");
			return;
		}
	}
}
