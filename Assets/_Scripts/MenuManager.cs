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


	private const string cardboardString = "cardboard";

	void Start () {
		StartCoroutine (LoadDevice ("None"));
	}

	void Update ()  {
		deviceName.text = XRSettings.loadedDeviceName;
	}

	public void XRSwitcher ()
	{
		if (XRSettings.loadedDeviceName == cardboardString) {
			XRSettings.LoadDeviceByName (cardboardString);
			VRPlayer.GetComponent<GyroController> ().enabled = false;
		} else if (XRSettings.loadedDeviceName != cardboardString) {
			XRSettings.LoadDeviceByName ("none");
			VRPlayer.GetComponent<GyroController> ().enabled = false;
		}
	}

	public void ToggleXR () {
		if (XRSettings.loadedDeviceName == "cardboard") {
			StartCoroutine (LoadDevice ("None"));
		} else
			StartCoroutine (LoadDevice ("cardboard"));
	}

	IEnumerator LoadDevice (string newDevice)
	{
		XRSettings.LoadDeviceByName (newDevice);
		yield return null;
		XRSettings.enabled = true;
	}

	public void PlayGame () 
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);	
	}
}
