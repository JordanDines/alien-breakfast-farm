using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public GameObject ingredientPrefab;


	public void RespawnIngredient () {
		if (transform.childCount == 0) {
			Instantiate (ingredientPrefab, transform.position, transform.rotation, this.gameObject.transform);
		}
	}

}
