using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStaticBool : MonoBehaviour {

    public static bool isVR;


    private void Awake()
    {
        QualitySettings.SetQualityLevel(0, true);
    }
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
            // Loads the VR Quality Settings
            QualitySettings.SetQualityLevel(1, true);
            Debug.Log(UnityEngine.QualitySettings.GetQualityLevel()+" VR Quality");
        } else if (isVR == true) {
            isVR = false;
            // Loads the NonVR Quality Settings
            QualitySettings.SetQualityLevel(0, true);
            Debug.Log(UnityEngine.QualitySettings.GetQualityLevel()+" Non VR Quality");
        }

        Debug.Log(isVR);
    }
}
