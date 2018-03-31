using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public GameObject ingredientPrefab;


	public void RespawnIngredient () {
		Debug.Log (transform.name + ": " + transform.childCount);
		if (transform.childCount == 0) {
			Instantiate (ingredientPrefab, transform.position, transform.rotation);
		}
	}

}
