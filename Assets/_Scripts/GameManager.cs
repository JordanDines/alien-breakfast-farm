using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public bool infinite;
	[HideInInspector]
	//If the player can hold something
	public bool canHold = true;
	[HideInInspector]
	//If the player is holding something and can place it
	public bool canPlace = false;
	//Reference to the currently held object
	//[HideInInspector]
	public GameObject holdingObject;
	//[HideInInspector]
	public GameObject previousObject;

	[Tooltip ("Reference the PlateUp GameObject")]
	//Reference to the plate up area
	public GameObject plateUp;
	public GameObject plateUpButton;

	[HideInInspector]
	public bool breakfastReady = false;
	[HideInInspector]
	public bool isPlating = false;


	public List<Recipe> recipes = new List<Recipe> ();
	private int recipeIndex = 0;
	private int ingredientIndex = 0;

	[HideInInspector]
	public List<Ingredient> currentNeededIngredients = new List<Ingredient> ();

	[HideInInspector]
	public Recipe currentRecipe;
	[HideInInspector]
	public List<Ingredient> currentlyPlated = new List<Ingredient> ();

	public GameObject ingredientPanel;
	public GameObject finishedPanel;


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
		currentRecipe = recipes [recipeIndex];
		PlateUpInteractive ();

		if (breakfastReady) {
			plateUpButton.GetComponent <Collider> ().enabled = true;
		}
		if (!breakfastReady) {
			CheckRecipe ();
		}
		if (!infinite && recipeIndex == recipes.Count - 1) {
			finishedPanel.SetActive (true);
			ingredientPanel.SetActive (false);
		}
	}

	void CreateNewRecipe ()
	{
		currentRecipe = recipes [recipeIndex];
		foreach (Ingredient ingredient in currentRecipe.ingredientsInRecipe) {
			if (currentRecipe.ingredientsInRecipe.Contains (ingredient)) {
				currentNeededIngredients.Add (ingredient);
				Instantiate (ingredient.notPlatedSprite, ingredientPanel.transform.position, ingredientPanel.transform.rotation, ingredientPanel.transform);
			}
		}
		// check if currentlyPlatedIngredients covers everything needed by currentNeededIngredients
		int numItemsNeeded = currentNeededIngredients.Count;   // (now we know how many things are needed in the meal)  
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
				break;
			}
		}
	}

	public void InfiniteNextRecipe () {
		//Clears the ingredients panel
		int ingCount = ingredientPanel.transform.childCount;
		Transform tempIng;
		for (int i = 0; i < ingCount; i++) {
			tempIng = ingredientPanel.transform.GetChild (i);
			Destroy (tempIng.gameObject);
		}

		//Clears the served items from the game world
		int servedItems = plateUp.GetComponent<PlateUp> ().placePoint.transform.childCount;
		Transform tempServed;
		for (int i = 0; i < servedItems; i++) {
			tempServed = plateUp.GetComponent<PlateUp> ().placePoint.transform.GetChild (i);
			Destroy (tempServed.gameObject);
		}

		recipeIndex = Random.Range(1, (recipes.Count - 1));
		currentNeededIngredients.Clear ();
		currentlyPlated.Clear ();
		previousObject = null;
		plateUpButton.GetComponent <Collider> ().enabled = false;
		breakfastReady = false;
		CreateNewRecipe ();
	}


	public void NextRecipe ()
	{
		//Clears the ingredients panel
		int ingCount = ingredientPanel.transform.childCount;
		Transform tempIng;
		for (int i = 0; i < ingCount; i++) {
			tempIng = ingredientPanel.transform.GetChild (i);
			Destroy (tempIng.gameObject);
		}

		//Clears the served items from the game world
		int servedItems = plateUp.GetComponent<PlateUp> ().placePoint.transform.childCount;
		Transform tempServed;
		for (int i = 0; i < servedItems; i++) {
			tempServed = plateUp.GetComponent<PlateUp> ().placePoint.transform.GetChild (i);
			Destroy (tempServed.gameObject);
		}

		recipeIndex++;
		currentNeededIngredients.Clear ();
		currentlyPlated.Clear ();
		previousObject = null;
		plateUpButton.GetComponent <Collider> ().enabled = false;
		breakfastReady = false;
		CreateNewRecipe ();
	}

	void PlateUpInteractive ()
	{
		//Checks if the currently held object is ready to be plated up
		if (holdingObject != null && (holdingObject.GetComponent<ObjectInteract> ().isReady == true || holdingObject.GetComponent<ObjectInteract> ().ingredient.needsToBeCooked == false)) {
			//Make the plate up area interactable
			plateUp.GetComponent<PlateUp>().plateUpGlow.SetActive(true);
			plateUp.GetComponent<Collider> ().enabled = true;
		} else {//if (holdingObject != null && holdingObject.GetComponent<ObjectInteract> ().isReady == false) {
			//Make it not interactable
			plateUp.GetComponent<PlateUp>().plateUpGlow.SetActive(false);
			plateUp.GetComponent<Collider> ().enabled = false;
			//}
		}
	}

	public void NextScene () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void RestartScene () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

}
