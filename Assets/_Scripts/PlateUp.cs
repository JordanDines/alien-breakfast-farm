using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateUp : MonoBehaviour {

	public ObjectInteract oi;
	private GameManager gm;

	public ObjectInteract heldObject;

	public GameObject holdingPoint1;

	public float grabbingSpeed = 1;

	public bool isPlating = false;

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

	}


	void MoveTowardsPlateUp () {
		//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)

		gm.holdingObject.transform.rotation = holdingPoint1.transform.rotation;

		gm.holdingObject.transform.position = Vector3.Lerp 
			(gm.holdingObject.transform.position, holdingPoint1.transform.position, grabbingSpeed);
		//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
		if (Vector3.Distance (gm.holdingObject.transform.position, holdingPoint1.transform.position) < .1f) {
			isPlating = false;
			gm.holdingObject.GetComponent<ObjectInteract> ().interactable = false;
			gm.canHold = true;
			gm.canPlace = false;
			gm.holdingObject = null;
		}

	}


	public void PlaceFood () {
		if (gm.holdingObject.GetComponent<ObjectInteract> ().isReady) {
			oi = gm.holdingObject.GetComponent<ObjectInteract> ();
			isPlating = true;
			//button.transform.GetComponent<Collider> ().enabled = true;
			//button.transform.GetComponent <Appliances> ().tempHeldObj = heldObject;
			//isPlacing = true;
			gm.holdingObject.transform.SetParent (holdingPoint1.transform.parent);
			transform.GetComponent<Collider> ().enabled = false;
		} else if (!gm.holdingObject.GetComponent<ObjectInteract> ().isReady) {
			Debug.Log ("Sorry, this food is not yet ready. Try cooking it, idiot");
			return;
		}
	}

}
