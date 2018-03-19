using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour {

	public GameObject holdingPoint;
	public GameObject objectPoint;
	public float grabbingSpeed = 1;
	public GameObject holdingParent;

	public bool isMoving = false;
	public bool isHeld = false;
	public bool isPlacing = false;

	private GameManager gameManager;

	void Start () {
		holdingPoint = GameObject.FindGameObjectWithTag ("HoldingPoint");
	}

	void Update () {
		MoveTowardsPlayer ();
		MoveTowardsPlacement ();
		HoldingObject ();

		print (holdingPoint.transform.childCount);
	}


	#region BUTTONS
	public void GrabObject () {
		if (isHeld && transform.tag == "Interactable") {
			isHeld = false;
			isPlacing = true;
			transform.SetParent (null);
			Debug.Log ("HIT" + transform.name);
		} else if(!isHeld && transform.tag == "Box") {
			isMoving = true;
			transform.SetParent (holdingParent.transform);
		}
		else if (isHeld) {
			isHeld = false;
			transform.SetParent (null);
			gameObject.GetComponent<Collider> ().enabled = true;
			gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
	}
	#endregion

	#region PLAYER_INTERACTIONS
	void MoveTowardsPlacement () {
		if (isPlacing) {
			transform.position = Vector3.Lerp (transform.position, objectPoint.transform.position, grabbingSpeed);
			if (transform.position == objectPoint.transform.position) {
				isPlacing = false;
			}
		}
	}

	void MoveTowardsPlayer () {
		//Check is the object has been picked up
		if (isMoving) {
			//Turn off the rigidbody and collider
			gameObject.GetComponent<Rigidbody> ().useGravity = false;
			gameObject.GetComponent<Collider> ().enabled = false;
			//Then move it to the player and make it look at the player
			transform.position = Vector3.Lerp (transform.position, holdingPoint.transform.position, grabbingSpeed);
			transform.LookAt(GameObject.FindGameObjectWithTag ("Player").transform.position);
			//If it has reached it's holding point, allow it to be interactable
			if (transform.position == holdingPoint.transform.position) {
				isHeld = true;
				isMoving = false;
			}
		}
	}

	void HoldingObject () {
		if (isHeld) {
			transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform.position);
		}
	}
	#endregion
		
}
