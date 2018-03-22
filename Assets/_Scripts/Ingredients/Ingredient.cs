using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject {
	[Header ("Cooking Variables")]
	public float grabbingSpeed = .1f;
	public int timeToCook;

	[Header ("Tags")]
	public string tagThisAs;
	//public string tagToInteractWith;

	public List<string> tagsToInteractWith = new List<string>();




}

