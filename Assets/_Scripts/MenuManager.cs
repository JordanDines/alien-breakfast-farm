using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private GameObject playerInScene;

	private const string cardboardString = "cardboard";

	public void XRSwitcher ()
	{
		if (XRSettings.loadedDeviceName == cardboardString) {
			XRSettings.LoadDeviceByName (cardboardString);
			Debug.Log (XRSettings.loadedDeviceName);
		} else if (XRSettings.loadedDeviceName != cardboardString) {
			XRSettings.LoadDeviceByName ("none");
			Debug.Log (XRSettings.loadedDeviceName);
		}
	}

	public void PlayGame () 
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);	
	}
}
