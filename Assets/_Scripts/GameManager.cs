using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//If the player can hold something
	public bool canHold = true;
	//If the player is holding something and can place it
	public bool canPlace = false;
	//Reference to the currently held object
	public GameObject holdingObject;
	[Tooltip("Reference the PlateUp GameObject")]
	//Reference to the plate up area
	public GameObject plateUp;

	public Ingredient ingredient;
	public bool breakfastReady = false;

	public List<Recipe> recipes = new List<Recipe>();
	private int recipeIndex = 0;

	public Recipe currentRecipe;



	void Start () {
		//Locks the rotation of the screen so the home button is on the right
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		currentRecipe = recipes[recipeIndex];
	}


	void Update () {



		foreach (Recipe recipe in recipes) {
			if(plateUp.GetComponent<PlateUp>().currentlyPlated.Equals(recipe.ingredientsInRecipe)){
				Debug.Log(recipe);
				
				Debug.Log ("YOU DID IT");
			}
		}
		//}
		//if(FindObjectOfType<PlateUp>().currentlyPlated.Contains (currentRecipe.ingredientsInRecipe)){
		//	Debug.Log ("YOU DID IT");
		//	breakfastReady = true;
		//}
		//if (plateUp.GetComponent<PlateUp>().currentlyPlated.Exists(currentRecipe.ingredientsInRecipe)) {
		//}
		//for (int i = 0; i < currentRecipe.ingredientsInRecipe.Count; i++) {
		//	Debug.Log(plateUp.GetComponent<PlateUp>().currentlyPlated.Count);
		//	
		//}

		if(breakfastReady) {
			recipeIndex++;
			currentRecipe = recipes [recipeIndex];
			//breakfastReady = false;
		}
		//Checks if the currently held object is ready to be plated up
		if (holdingObject != null && (holdingObject.GetComponent<ObjectInteract> ().isReady == true|| holdingObject.GetComponent<ObjectInteract>().ingredient.needsToBeCooked == false)) {
			//Make the plate up area interactable
			plateUp.GetComponent<Collider> ().enabled = true;
		} else {//if (holdingObject != null && holdingObject.GetComponent<ObjectInteract> ().isReady == false) {
			//Make it not interactable
			plateUp.GetComponent<Collider> ().enabled = false;
		//}
		}
	}
}
