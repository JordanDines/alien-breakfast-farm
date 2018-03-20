using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliances : MonoBehaviour {

	private ObjectInteract oi;
	private GameManager gm;
	[SerializeField]
	private GameObject holdingPoint;

	public float grabbingSpeed = 1;

	private bool isPlacing = false;
	private bool isGrabbing = false;

	private bool inserting = false;
	private bool removing = false;
	private bool appIsHolding = false;
	//private 


	public GameObject holdingPoint1;
	public GameObject holdingPoint2;
	public GameObject heldObject;
	public GameObject tempHeldObj;


	public string holdingPointTag;

	void Start () {
		//Find the GameManager in the scene to reference later on
		gm = FindObjectOfType<GameManager> ();
		oi = FindObjectOfType<ObjectInteract> ();
		//Find the player's HoldingPoint to also reference later on
		holdingPoint = GameObject.FindGameObjectWithTag ("HoldingPoint");

		//Reference each of the holding points
		holdingPoint1 = GameObject.FindGameObjectWithTag (holdingPointTag).transform.GetChild (0).gameObject;
		holdingPoint2 = GameObject.FindGameObjectWithTag (holdingPointTag).transform.GetChild (1).gameObject;
		heldObject = null;
	}

	void Update () {
		MoveTowardsPlacement ();
		MoveTowardsPlayer ();
		MoveToHoldingPoint1 ();
		MoveToHoldingPoint2 ();

		CookFood ();
	}


	void CookFood () {
		if (appIsHolding) {
			//Wait for seconds before collider is turned back on, then call MoveToHoldingPoint2();
		}
	}
	void MoveTowardsPlacement () {
		//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)
		if (isPlacing) {
			gm.holdingObject.transform.rotation = holdingPoint1.transform.rotation;

			gm.holdingObject.transform.position = Vector3.Lerp 
				(gm.holdingObject.transform.position, holdingPoint1.transform.position, grabbingSpeed);
			//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
			if (Vector3.Distance (gm.holdingObject.transform.position, holdingPoint1.transform.position) < .1f) {
				
				gm.holdingObject.GetComponent<ObjectInteract> ().interactable = false;
				isPlacing = false;
				gm.canHold = true;
				gm.canPlace = false;
				gm.holdingObject = null;
			}
		}
	}

	void MoveTowardsPlayer () {
		//Check is the object has been picked up
		if (isGrabbing) {
			//Turn off the rigidbody and collider
			gm.holdingObject.GetComponent<Rigidbody> ().useGravity = false;
			gm.holdingObject.GetComponent<Collider> ().enabled = true;
			//Then move it to the player and make it look at the player
			gm.holdingObject.transform.position = Vector3.Lerp (gm.holdingObject.transform.position, holdingPoint.transform.position, grabbingSpeed);
			gm.holdingObject.transform.LookAt(GameObject.FindGameObjectWithTag ("Player").transform.position);
			//If it has reached the player's holding point, allow it to be put down in a specific spot
			if (Vector3.Distance(transform.position, holdingPoint.transform.position) < 0.1f) {
				gm.GetComponent<ObjectInteract> ().interactable = false;
			}
		}
	}

	void MoveToHoldingPoint1 () {
		if (inserting) {
			tempHeldObj.transform.position = Vector3.Lerp (tempHeldObj.transform.position, holdingPoint2.transform.position, grabbingSpeed);
			Debug.Log ("I made it this far");
			//holdingPoint1.transform.SetParent (holdingPoint2.transform);
			if (Vector3.Distance (tempHeldObj.transform.position, holdingPoint2.transform.position) < 0.1f) {
				Debug.Log ("I'm in");
				inserting = false;
				transform.GetComponent<Collider> ().enabled = true;
				appIsHolding = true;
			}
		}
	}


	void MoveToHoldingPoint2 () {
		if (removing) {
			heldObject.transform.position = Vector3.Lerp (holdingPoint2.transform.position, holdingPoint1.transform.position, grabbingSpeed);
			if (Vector3.Distance (holdingPoint2.transform.position, holdingPoint1.transform.position) < 0.1f) {
				Debug.Log ("I'm out");
				heldObject.GetComponent<Collider> ().enabled = true;
				removing = false;
			}
		}

	}
	#region TOAST
	public void ToastInsert () {
		if (gm.holdingObject.tag == "Toast") {
			heldObject = tempHeldObj;
			heldObject = gm.holdingObject;
			isPlacing = true;
			gm.holdingObject.transform.SetParent (holdingPoint1.transform.parent);
			transform.GetComponent<Collider> ().enabled = false;
			GameObject.FindGameObjectWithTag ("Button").transform.GetComponent<Collider> ().enabled = true;
			GameObject.FindGameObjectWithTag ("Button").transform.GetComponent <Appliances> ().tempHeldObj = heldObject;
		} else if (gm.holdingObject.tag != "Toast") {
			return;
		}
	}

	public void ToastButton () {
			inserting = true;
			transform.GetComponent<Collider> ().enabled = false;
			Debug.Log ("This works sorta");
		} 

	public void ToastRemove() {
		
	}
	#endregion 
}
