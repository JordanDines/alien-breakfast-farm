using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGyroScript : MonoBehaviour {

    public GameObject playerCamera;
  //  private Component gyroScript;
    private bool vrValue;
 
    void Start() {
     //   gyroScript = playerCamera.GetComponent<GyroController>();
        vrValue = SetStaticBool.isVR;

        ToggleGyro();
    }


    private void LateUpdate()
    {
        Debug.Log(vrValue);
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
