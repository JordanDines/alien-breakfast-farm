using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private GameObject playerInScene;

	public Text vrSettingStateMessage;
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
}
