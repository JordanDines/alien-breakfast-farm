using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour {

	private GameManager gm;
	[Header("References")]
	[SerializeField]
	private GameObject holdingPoint;

	[Header("Variables")]
	[SerializeField]
	private float grabbingSpeed = 1;
	[SerializeField]
	private bool isPlacing = false;
	[SerializeField]
	private bool isGrabbing = false;

	public bool hasBeenPlaced = false;

	public bool interactable = true;


	void Start () {
		//Find the GameManager in the scene to reference later on
		gm = FindObjectOfType<GameManager> ();
		//Find the player's HoldingPoint to also reference later on
		holdingPoint = GameObject.FindGameObjectWithTag ("HoldingPoint");
		//Initialize the object currently being held in the GameManager
		gm.holdingObject = null;

	}

	void Update () {
		//These Functions allow the objects to move after the events trigger as buttons are static
		MoveTowardsPlayer ();
		MoveTowardsPlacement ();
		HoldingObject ();
	}
	#region BUTTONS

	public void GrabObject () {
		//If you're not holding anything, and you're looking at a valid target to be picked up, pick the object up (See MoveTowardsPlayer())
		//&& transform.tag == "Box"
		if(interactable && gm.canHold && transform.tag != "Toaster") {
			isGrabbing = true;
			//Set it's parent to your holdingPoint
			transform.SetParent (holdingPoint.transform);
			gm.holdingObject = gameObject;
		}
	}

	public void PlaceObject () {
		//If you're holding something and you're looking at a valid target, put the object down (See MoveTowardsPlacement())
		if (gm.canPlace && (transform.tag != "Box" || transform.tag != "Toast")) {
			isPlacing = true;
			//Also set it to the specific target gameobject's PlacePoint parent
			gm.holdingObject.transform.SetParent (transform.Find ("PlacePoint").transform);
		}
	}
	#endregion

	#region PLAYER_INTERACTIONS

	void DropObject () {

	}

	void MoveTowardsPlacement () {
		//if the object is moving toward a PlacePoint, move it to the position and snap the rotation (cannot get Quaternion.Lerp working)
		if (isPlacing) {
			gm.holdingObject.transform.rotation = transform.Find ("PlacePoint").transform.rotation;
			gm.holdingObject.transform.position = Vector3.Lerp (gm.holdingObject.transform.position, transform.Find("PlacePoint").transform.position, grabbingSpeed);
			//Also enable the collider to allow it to be interactable again
			gm.holdingObject.GetComponent<Collider> ().enabled = true;
			//If it gets close enough to the desired location, stop it moving and allow it to be picked up again
			if (Vector3.Distance (gm.holdingObject.transform.position, transform.Find("PlacePoint").transform.position) < .1f) {
				hasBeenPlaced = true;
				isPlacing = false;
				gm.canPlace = false;
				gm.canHold = true;
				gm.holdingObject = null;

			}
		}
	}

	void MoveTowardsPlayer () {
		//Check is the object has been picked up
		if (isGrabbing) {
			//Turn off the rigidbody and collider
			gameObject.GetComponent<Rigidbody> ().useGravity = false;
			gameObject.GetComponent<Collider> ().enabled = false;
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
	}

	void HoldingObject () {
		//If you're holding the object, make it look at the player. Will follow the player's rotation
		if (gm.canPlace) {
			gm.holdingObject.transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform.position);
			gm.holdingObject.transform.rotation = Camera.main.GetComponent<Transform> ().transform.rotation;
		}
	}
	#endregion
		
}
