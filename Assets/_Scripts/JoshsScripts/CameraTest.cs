using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraTest : MonoBehaviour {

    public Camera Camera;

	// Use this for initialization
	void Start () {
        UnityEngine.XR.XRSettings.enabled = false;

        Camera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
       Camera.GetComponent<Transform>().localRotation = UnityEngine.XR.InputTracking.GetLocalRotation(XRNode.CenterEye);
    }
}
