using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGyroScript : MonoBehaviour {

    public GameObject playerCamera;
    private bool vrValue;


    private void Awake()
    {
       // vrValue = SetStaticBool.isVR;

        //ToggleGyro();
    }
    void Start() {
 
       
    }


    private void LateUpdate()
    {
       // Debug.Log(vrValue);
       // Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void ToggleGyro()
    {

        if (vrValue == true)
        {
            playerCamera.GetComponent<GyroController>().enabled = false;
        }
        else if (vrValue == false)
        {
            playerCamera.GetComponent<GyroController>().enabled = true;

        }
    }
}
