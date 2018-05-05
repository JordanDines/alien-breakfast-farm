using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private GameObject VRPlayer;


	[SerializeField]
	private Text deviceName;
	[SerializeField]


	private const string cardboardString = "cardboard";

	void Update ()  {
		deviceName.text = XRSettings.loadedDeviceName;
	}

	public void XRSwitcher ()
	{
		if (XRSettings.loadedDeviceName == cardboardString) {
			XRSettings.LoadDeviceByName (cardboardString);
			VRPlayer.GetComponent<GyroController> ().enabled = false;
			Debug.Log (XRSettings.loadedDeviceName);
		} else if (XRSettings.loadedDeviceName != cardboardString) {
			XRSettings.LoadDeviceByName ("none");
			Debug.Log (XRSettings.loadedDeviceName + ": means nothing m9");
			VRPlayer.GetComponent<GyroController> ().enabled = false;
		}
	}

	public void PlayGame () 
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);	
	}
}
