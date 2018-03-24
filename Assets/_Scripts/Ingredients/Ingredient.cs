using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject {
	[Header ("Cooking Variables")]
	//How long it takes for the object to move to the player
	[Tooltip("How long it takes for the object to move to the player")]
	public float grabbingSpeed = .1f;
	//How long this ingredient takes to become ready
	[Tooltip("How long this ingredient takes to become ready")]
	public int timeToCook;
	//Set this to true if the ingredient needs to be cooked before plating up
	[Tooltip("Set this to true if the ingredient needs to be cooked before plating up")]
	public bool needsToBeCooked;

	[Header ("Tags")]
	//MAKE SURE THE TAGS EXIST IN THE PROJECT
	//Tag this ingredient as...
	[Tooltip("Make sure the tag exists in the project before setting this")]
	public string tagThisAs;

	[Tooltip("A list of all of the objects this ingredient can interact with")]
	//List of object's tags that this ingredient can interact with
	public List<string> tagsToInteractWith = new List<string>();

}

