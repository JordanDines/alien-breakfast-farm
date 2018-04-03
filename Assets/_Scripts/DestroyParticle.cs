using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour {

	public float timeToDestory;

	// Update is called once per frame
	void Update () {
		Destroy (this.gameObject, timeToDestory);
	}
}
