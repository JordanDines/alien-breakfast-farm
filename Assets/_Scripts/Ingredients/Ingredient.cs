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
	[Tooltip("What name to give this ingredient once it gets cooked")]
	public string cookedName;


	[Header ("Tags")]
	//MAKE SURE THE TAGS EXIST IN THE PROJECT
	//Tag this ingredient as...
	[Tooltip("Make sure the tag exists in the project before setting this")]
	public string tagThisAs;

	[Tooltip("A list of all of the objects this ingredient can interact with")]
	//List of object's tags that this ingredient can interact with
	public List<string> tagsToInteractWith = new List<string>();

}
//[CustomEditor(typeof(Ingredient))]
//public class IngredientCustomEditor:Editor {
//
//override public void OnInspectorGUI () {
//	
//		Ingredient myTarget = (Ingredient)target;
//
//		GUILayout.BeginHorizontal ();
//		GUILayout.Label ("Does this need to be cooked?", GUILayout.Width (100));
//		myTarget.needsToBeCooked = EditorGUILayout.Toggle (myTarget.needsToBeCooked);
//		GUILayout.EndHorizontal ();
//
//		if (myTarget.needsToBeCooked) {
//			GUILayout.BeginHorizontal ();
//			GUILayout.Label ("Name for cooked ingredient", GUILayout.Width (70));
//			myTarget.newName = EditorGUILayout.TextField (myTarget.newName);
//			GUILayout.EndHorizontal ();
//		} else {
//			return;
//		}
//	}
//
//}