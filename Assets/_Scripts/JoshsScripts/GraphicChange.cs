using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphicChange : MonoBehaviour {

    public string vRLevelToLoad;
    public string phoneLevelToLoad;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNonVR()
    {
        {
            // Loads the NonVR Quality Settings
            UnityEngine.QualitySettings.SetQualityLevel(0, true);
            Debug.Log(UnityEngine.QualitySettings.GetQualityLevel());
            SceneManager.LoadScene(phoneLevelToLoad);
        }
    }

    public void LoadVR()
    {
        {
            // Loads the Cardboard Quality Settings
            UnityEngine.QualitySettings.SetQualityLevel(1, true);
            Debug.Log(UnityEngine.QualitySettings.GetQualityLevel());
            SceneManager.LoadScene(vRLevelToLoad);
    }
    }
        
}
