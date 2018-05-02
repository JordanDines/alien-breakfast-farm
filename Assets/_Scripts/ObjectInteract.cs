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
	private float grabbingSpeed;
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
	[Tooltip("Drag in this objects 'Staple Point' GameObject which will reference the position that the next item will be stacked at")]
	public GameObject staplePoint;
	[HideInInspector]
	public GameObject respawnParent;

	void Start () {
		respawnParent = this.gameObject.transform.parent.gameObject;
		//Find the GameManager in the scene to reference later on
		gm = FindObjectOfType<GameManager> ();
		//Find the player's HoldingPoint to also reference later on
		holdingPoint = GameObject.FindGameObjectWithTag ("HoldingPoint");
		//Set this objects tag to the tag it needs to be
		transform.tag = ingredient.tagThisAs;

		grabbingSpeed = gm.grabbingSpeed;
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
		if (gm.holdingObject == null && interactable == true) {
			transform.GetComponent<Collider> ().enabled = true;
		} else {
			transform.GetComponent<Collider> ().enabled = false;
		}
	}

	void MoveTowardsPlayer () {
		//Then move it to the player and make it look at the player
		transform.position = Vector3.Lerp (transform.position, holdingPoint.transform.position, grabbingSpeed * Time.deltaTime);
		gm.holdingObject.transform.LookAt(GameObject.FindGameObjectWithTag ("Player").transform.position);
		hasBeenPlaced = false;
		gm.canHold = false;
		//If it has reached the player's holding point, allow it to be put down in a specific spot
		if (Vector3.Distance(transform.position, holdingPoint.transform.position) < 0.007f) {
			isGrabbing = false;
			gm.canPlace = true;
		}
	}


	void HoldingObject () {
		if (gm.holdingObject.GetComponent<ObjectInteract> ().isGrabbing) {
			//If you're holding the object, make it look at the player. Will follow the player's rotation
			gm.holdingObject.transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform.position);
			gm.holdingObject.transform.rotation = Camera.main.GetComponent<Transform> ().transform.rotation;
		}
	}
	#endregion
		
}
