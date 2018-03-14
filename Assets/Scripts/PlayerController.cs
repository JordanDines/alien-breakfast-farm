using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject ground;
	private bool walking = false;
	private Vector3 spawnPoint;
	public float moveSpeed;

	void Start () {
		// set spawnPoint when you start
		spawnPoint = transform.position;
	}
	


	//void Update () {
	//	// if walking, move forwards the way the camera is facing
	//	if (walking) {
	//		transform.position = transform.position + Camera.main.transform.forward * moveSpeed * Time.deltaTime;
	//	}
	//
	//
	//	// if ray (looking forwards) looks at ground, stop (otherwise, walk)
	//	Ray ray = Camera.main.ViewportPointToRay (new Vector3 (.5f, .5f, 0));
	//	RaycastHit hit;
	//
	//	if (Physics.Raycast (ray, out hit)) {
	//		if (hit.collider.name.Contains ("Ground")) {
	//			walking = false;
	//		} else {
	//			walking = true;
	//		}
	//	}
	//
	//
	//
	//	// if fall off world, respawn at spawnPoint
	//	if (transform.position.y < -10f) {
	//		transform.position = spawnPoint;
	//	}
	//
	//
	//}
}
