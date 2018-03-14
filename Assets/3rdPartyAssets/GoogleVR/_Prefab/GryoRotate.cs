using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GryoRotate : MonoBehaviour {

	private Gyroscope gyro;

	void Awake () {
		if (SystemInfo.supportsGyroscope)
		{
			gyro = Input.gyro;
			gyro.enabled = true;
		}
		else
		{
			Debug.Log("Phone doesen't support");
		}
	}

	void Update () 
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		player.transform.Rotate (-gyro.rotationRateUnbiased.x, -gyro.rotationRateUnbiased.y, gyro.rotationRateUnbiased.z);
	}

	void OnGUI()
	{
		GUILayout.Label ("Gyroscope attitude : " + gyro.attitude);
	}
}

