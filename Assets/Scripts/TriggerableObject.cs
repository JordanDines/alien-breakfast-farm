using UnityEngine;
using System.Collections;

public class TriggerableObject : MonoBehaviour {
	
	public Vector3 deactivatedPos;  // position for door at closed position
	public Vector3 activatedPos;    // position for door at opened position

	public bool gazedAtNow = false; // whether the door is beign gazed at or not


	void Start () {
		deactivatedPos = transform.position;
		activatedPos = transform.position + (Vector3.up * 2f);
	}
	

	void Update () {
	
		if (gazedAtNow) {  // if it's being gazed at
			if (transform.position.y < activatedPos.y){
			transform.position = transform.position + (Vector3.up * 1f * Time.deltaTime);
			}
		} else {           // if it's not being gazed at
			if (transform.position.y > deactivatedPos.y) {
				transform.position = transform.position + (Vector3.down * 0.3f * Time.deltaTime);
			}
		}

		// if door closes again, set to white
		if (transform.position.y <= deactivatedPos.y){
		GetComponent<Renderer> ().material.color = Color.white;
		}
	}



	// these functions are called from the Event Trigger on the door, on PointerEnter and PointerExit

	// if gazed at, change color to blue, and set gazedAtNow = true
	public void ActivateNow(){
		GetComponent<Renderer> ().material.color = Color.blue;
		gazedAtNow = true;

	}


	// if not gazed at, set gazedAtNow = false
	public void DeactivateNow(){
		
		gazedAtNow = false;

	}


}
