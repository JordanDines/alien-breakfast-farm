using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	//If the player can hold something
	public bool canHold = true;
	//If the player is holding something and can place it
	public bool canPlace = false;
	//Reference to the currently held object
	public GameObject holdingObject;
	[Tooltip ("Reference the PlateUp GameObject")]
	//Reference to the plate up area
	public GameObject plateUp;
	public GameObject plateUpButton;

	//public Ingredient ingredient;
	public bool breakfastReady = false;
	public bool isPlating = false;


	public List<Recipe> recipes = new List<Recipe> ();
	private int recipeIndex = 0;
	private int ingredientIndex = 0;

	public List<Ingredient> currentNeededIngredients = new List<Ingredient> ();

	public Recipe currentRecipe;

	public List<Ingredient> currentlyPlated = new List<Ingredient> ();

	public GameObject ingredientPanel;

	void Start ()
	{
		

		//Locks the rotation of the screen so the home button is on the right
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		currentRecipe = recipes [recipeIndex];
		foreach (Ingredient ingredient in currentRecipe.ingredientsInRecipe) {
			if (currentNeededIngredients.Count >= currentRecipe.ingredientsInRecipe.Count) {
				breakfastReady = false;
				Debug.Log ("Got them adding to list");
				break;
			} else {
				if (currentRecipe.ingredientsInRecipe.Contains (ingredient)) {
					currentNeededIngredients.Add (ingredient);
					Instantiate (ingredient.notPlatedSprite, ingredientPanel.transform.position, ingredientPanel.transform.rotation, ingredientPanel.transform);
					//ingredientScreen.transform.GetComponentInChildren<GameObject>().SetActive(true);
					//ingredientScreen.transform.GetComponentInChildren<Image> ().sprite = ingredient.notPlatedSprite;

				}
			}

		}
		//for (int i = 0; i < ingCount; i++) {
		//}

	}


	void Update ()
	{

		if (isPlating) {

		}
		currentRecipe = recipes [recipeIndex];
		PlateUpInteractive ();

		if (breakfastReady) {
			plateUpButton.GetComponent <Collider> ().enabled = true;
		}
		if (!breakfastReady) {
			CheckRecipe ();
		}
		 

	}



	void CreateNewRecipe ()
	{

		//foreach (Ingredient oldIng in currentlyPlated) {
		//
		//	if(currentRecipe.ingredientsInRecipe.Contains(oldIng)) {
		//		Debug.Log ("I should destroy this");
		//	}
		//	//if (!currentlyPlated.Contains (oldIng)) {
		//	//	break;
		//	//} else 
		//	//	if (currentlyPlated.Contains (oldIng)){
		//	//	currentlyPlated.Remove (oldIng);
		//	//	currentNeededIngredients.Remove (oldIng);
		//	//}
		//}


		currentRecipe = recipes [recipeIndex];
		foreach (Ingredient ingredient in currentRecipe.ingredientsInRecipe) {
			//if (currentNeededIngredients.Count > currentRecipe.ingredientsInRecipe.Count) {
			//	breakfastReady = false;
			//	break;
			//} else 
		
			if (currentRecipe.ingredientsInRecipe.Contains (ingredient)) {
				currentNeededIngredients.Add (ingredient);
				Instantiate (ingredient.notPlatedSprite, ingredientPanel.transform.position, ingredientPanel.transform.rotation, ingredientPanel.transform);
			}
		}
		
		//currentNeededIngredients.Clear();
		//currentlyPlated.Clear();

		// check if currentlyPlatedIngredients covers everything needed by currentNeededIngredients
		int numItemsNeeded = currentNeededIngredients.Count;   // (now we know how many things are needed in the meal)  

		//currentNeededIngredients = currentRecipe.ingredientsInRecipe;
		//foreach (Ingredient ingredient in currentRecipe.ingredientsInRecipe) {
		//	currentNeededIngredients.Add (ingredient);
		//}

		// currentNeededIngredients.Clear();
		// for each ingredient in new recipe
		// currentNeededIngredients.Add(ingredient);


	}





	// runs every frame, checks if the meal has been made -> if so, button becomes active
	void CheckRecipe ()
	{
		// check if currentlyPlatedIngredients covers everything needed by currentNeededIngredients
		int numItemsNeeded = currentNeededIngredients.Count;   // (now we know how many things are needed in the meal)  
		int numItemsCorrect = 0;

		// for each ingredient in currentNeededIngredients, if currentlyPlatedIngredients contains that ingredient, do nothing, if not,  
		foreach (Ingredient ingredient in currentNeededIngredients.ToArray()) {
			if (currentlyPlated.Contains (ingredient)) {
				numItemsCorrect++;
				int ingCount = ingredientPanel.transform.childCount;
				Transform tempGO;
				for (int i = 0; i < ingCount; i++) {
					tempGO = ingredientPanel.transform.GetChild (i);
					 if (ingredient.tagThisAs == tempGO.tag) {
						Instantiate (ingredient.platedSprite, ingredientPanel.transform.position, ingredientPanel.transform.rotation, ingredientPanel.transform);
						Destroy (tempGO.gameObject);
					}
				}

			}
			if (numItemsCorrect == numItemsNeeded) {
				breakfastReady = true;
				numItemsCorrect = 0;

			}
		}
	}





	public void NextRecipe ()
	{

		//List<GameObject> panel = new List<GameObject>;
		//foreach (GameObject child in panel.ToArray()) {
		//
		//}
		int ingCount = ingredientPanel.transform.childCount;
		Transform tempGO;
		for (int i = 0; i < ingCount; i++) {
			tempGO = ingredientPanel.transform.GetChild (i);
			Destroy (tempGO.gameObject);
		}

		recipeIndex++;
		currentNeededIngredients.Clear ();
		currentlyPlated.Clear ();

		plateUpButton.GetComponent <Collider> ().enabled = false;
		breakfastReady = false;
		CreateNewRecipe ();
		Debug.Log (currentRecipe);
	}

	void PlateUpInteractive ()
	{
		//Checks if the currently held object is ready to be plated up
		if (holdingObject != null && (holdingObject.GetComponent<ObjectInteract> ().isReady == true || holdingObject.GetComponent<ObjectInteract> ().ingredient.needsToBeCooked == false)) {
			Debug.Log ("The plate can be used");
			//Make the plate up area interactable
			plateUp.GetComponent<Collider> ().enabled = true;
		} else {//if (holdingObject != null && holdingObject.GetComponent<ObjectInteract> ().isReady == false) {
			//Make it not interactable
			plateUp.GetComponent<Collider> ().enabled = false;
			//}
		}
	}

}
