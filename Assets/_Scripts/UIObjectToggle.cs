using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectToggle : MonoBehaviour
{
    /// <summary>
    /// Variables
    /// </summary>
    
    public List<GameObject> go;
    
    /// <summary>
    /// Functions
    /// </summary>
    public void DoToggle()
    {//get each gameobject in the list
        foreach (GameObject go in go)
        {
            if (go.activeInHierarchy == true) // if the object is currently active set them to inactive.
            {
                go.SetActive(false);
            }
            else go.SetActive(true); // if the object is already inactive, make them active.
        }
    }
}
