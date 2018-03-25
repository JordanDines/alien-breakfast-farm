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
	private int ingredientIndex = 0;

	public List<Ingredient> currentNeededIngredients = new List<Ingredient> ();

	public Recipe currentRecipe;

	public List<Ingredient> currentlyPlated = new List<Ingredient>();


	void Start () {
		//Locks the rotation of the screen so the home button is on the right
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		currentRecipe = recipes[recipeIndex];

		foreach (Ingredient ingredient in currentRecipe.ingredientsInRecipe) {
			currentNeededIngredients.Add (ingredient);
		}
	}


	void Update () {


		foreach (Ingredient ingredient in currentRecipe.ingredientsInRecipe) {
			if (currentNeededIngredients.Count >= currentRecipe.ingredientsInRecipe.Count) {
				Debug.Log ("Got them adding to list");
				return;
			} else {
				if (currentRecipe.ingredientsInRecipe.Contains (ingredient)) {
					currentNeededIngredients.Add (ingredient);

				}
			}
		}


		//foreach (Recipe recipe in recipes) {
		//	if(currentlyPlated.Equals(recipe.ingredientsInRecipe)){
		//		Debug.Log(recipe);
		//		
		//		Debug.Log ("YOU DID IT");
		//	}
		//}
		//}


		//if(currentlyPlated.FindAll (currentNeededIngredients)){
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
			foreach (Ingredient ingredient in currentRecipe.ingredientsInRecipe) {
				currentNeededIngredients.Add (ingredient);
			}
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
