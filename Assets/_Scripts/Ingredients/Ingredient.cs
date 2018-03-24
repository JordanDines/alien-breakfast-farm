using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject {
	[Header ("Cooking Variables")]
	//How long it takes for the object to move to the player
	public float grabbingSpeed = .1f;
	//How long this ingredient takes to become ready
	public int timeToCook;

	[Header ("Tags")]
	//MAKE SURE THE TAGS EXIST IN THE PROJECT
	//Tag this ingredient as...
	public string tagThisAs;

	//List of object's tags that this ingredient can interact with
	public List<string> tagsToInteractWith = new List<string>();

}

