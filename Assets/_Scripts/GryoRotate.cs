using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GryoRotate : MonoBehaviour {

	private Gyroscope gyro;
	public float smooth = 10;

	Quaternion lastGyro;
	Quaternion currentGyro;

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
		currentGyro = gyro.attitude;
		if (currentGyro != lastGyro) {
			//transform.rotation = Quaternion.Slerp (transform.rotation, new Quaternion (gyro.rotationRateUnbiased.x, gyro.rotationRateUnbiased.y, gyro.rotationRateUnbiased.z, gyro.attitude.w), Time.deltaTime * smooth);

			//transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.w, Input.gyro.attitude.z, Input.gyro.attitude.y), Time.deltaTime * smooth);
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			player.transform.Rotate ((-gyro.rotationRateUnbiased.x / smooth), -gyro.rotationRateUnbiased.y, (gyro.rotationRateUnbiased.z / smooth));
		} else {
			return;
		}

		lastGyro = currentGyro;
	}

	void OnGUI()
	{
		GUILayout.Label ("Gyroscope attitude : " + gyro.attitude);
	}
}