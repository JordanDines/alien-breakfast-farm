using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStaticBool : MonoBehaviour {

    public static bool isVR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoStaticBool()
    {
        if (isVR == false)
        {
            isVR = true;
        } else if (isVR == true) {
            isVR = false;
        }

        Debug.Log(isVR);
    }
}
