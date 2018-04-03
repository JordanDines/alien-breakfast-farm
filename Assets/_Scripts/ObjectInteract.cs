using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gvr;
using UnityEngine.EventSystems;

public class ObjectInteract : MonoBehaviour {
	[Header ("Ingredient to Use")]
	public Ingredient ingredient;

	[HideInInspector]
	private GameObject player;
	[HideInInspector]
	private GameManager gm;
	[HideInInspector]
	private GameObject holdingPoint;

	[Header ("Mesh Changes")]
	[Tooltip("Reference the uncooked GameObject that is a child of this GameObject")]
	public GameObject uncookedMesh;
	[Tooltip("Reference the cooked GameObject that is a child of this GameObject")]
	public GameObject cookedMesh;

	[Header("Variables")]
	[Tooltip("The speed at which the ingredient goes to the player")]
	private float grabbingSpeed = 1;
	[HideInInspector]
	private bool isGrabbing = false;
	[HideInInspector]
	public bool hasBeenPlaced = false;
	[HideInInspector]
	public bool interactable = true;
	[HideInInspector]
	public bool isReady;
	[Tooltip ("Change this offset to change the rotation when this item gets plated up")]
	public Vector3 offset;
	//This is a reference to the GvrReticlePointer's gameobject
	private GameObject reticle;
	[Tooltip("Drag in this objects 'Staple Point' GameObject which will reference the position that the next item will be stacked at")]
	public GameObject staplePoint;

	void Start () {
		//Find the object holding this script and get the gameobject as a reference
		reticle = FindObjectOfType<GvrReticlePointer> ().gameObject;
		//Find the GameManager in the scene to reference later on
		gm = FindObjectOfType<GameManager> ();
		//Find the player's HoldingPoint to also reference later on
		holdingPoint = GameObject.FindGameObjectWithTag ("HoldingPoint");
		//Initialize the object currently being held in the GameManager
		gm.holdingObject = null;
		//Set this objects tag to the tag it needs to be
		transform.tag = ingredient.tagThisAs;
	}


	//public void OnMouseEnter () {
	//	Debug.Log ("On");
	//	//When the cursor hovers over the ingredient, turn the reticle on
	//	reticle.GetComponent<MeshRenderer> ().enabled = true;
	//}
	//
	//public void OnMouseExit () {
	//	Debug.Log ("Off");
	//	//When the cursor leaves the ingredient, turn the reticle off
	//	reticle.GetComponent<MeshRenderer> ().enabled = false;
	//}

	void Update () {
		//These Functions allow the objects to move after the events trigger as buttons are static
		if (isGrabbing) 
		{
			MoveTowardsPlayer ();
		}
		//This function has been depreciated in this script. See Appliances.cs void MoveTowardPlacement(). 
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

		InteractiveCheck ();

	}
	#region BUTTONS

	public void GrabObject () {
		//If you're not holding anything, and you're looking at a valid target to be picked up, pick the object up (See MoveTowardsPlayer())
		if(interactable && gm.canHold && transform.tag == ingredient.tagThisAs) {
			//Turn off the rigidbody
			GetComponent<Rigidbody> ().useGravity = false;
			isGrabbing = true;
			interactable = false;
			//Set it's parent to your holdingPoint
			transform.SetParent (holdingPoint.transform);
			gm.holdingObject = gameObject;
		}
	}
		
	#endregion

	#region PLAYER_INTERACTIONS

	void FoodReady () {
		//The ol' switcheroo. If the food is ready, set the cooked GameObject to active whilst setting the uncooked Gameobject to inactive.
		uncookedMesh.SetActive (false);
		cookedMesh.SetActive (true);
	}

	void InteractiveCheck () {
		if (interactable == true) {
			GetComponent<Collider> ().enabled = true;
		} else if (interactable == false) {
			GetComponent<Collider> ().enabled = false;
		}

		if (gm.holdingObject == null && interactable == true) {
			transform.GetComponent<Collider> ().enabled = true;
		} else {
			transform.GetComponent<Collider> ().enabled = false;
		}
	}

	void DropObject () {

	}


	//This function has been depreciated in this script. See Appliances.cs void MoveTowardPlacement().
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
