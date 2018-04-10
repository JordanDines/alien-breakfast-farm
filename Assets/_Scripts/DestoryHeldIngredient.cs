using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryHeldIngredient : MonoBehaviour {

	private GameManager gm;

	void Start () {
		gm = FindObjectOfType<GameManager> ();
	}

	void Update () {
		if (gm.holdingObject != null) {
			GetComponent<Collider> ().enabled = true;
		} else
			GetComponent<Collider> ().enabled = false;
	}

	// Update is called once per frame
	public void DestoryThisIngredient () {
		gm.holdingObject.GetComponent<ObjectInteract> ().respawnParent.GetComponent<Respawn> ().RespawnIngredient ();
		Destroy (gm.holdingObject.gameObject);
		gm.holdingObject = null;
		gm.canHold = true;
		gm.canPlace = false;
	}
}
