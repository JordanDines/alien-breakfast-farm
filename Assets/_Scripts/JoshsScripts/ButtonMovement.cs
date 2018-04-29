using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour {

    public GameObject button;

    public GameObject buttonUpPos;
    public GameObject buttonDownPos;

    public float pressSpeed;

   [HideInInspector]
    public float timeCount;

    private bool timeForThing;
    // Use this for initialization
    void Start () {
                
	}
	
	// Update is called once per frame
	void Update () {

        if (timeForThing == true)
        {
            DoTheThing();
            timeCount += Time.deltaTime;
        }
        else
            timeCount = 0f;

    }

    public void DoTheThing() {

        timeForThing = true;
        print("Pressed");
        button.transform.position = Vector3.MoveTowards(buttonUpPos.transform.position, buttonDownPos.transform.position, pressSpeed);
 
        button.transform.position = Vector3.MoveTowards(buttonDownPos.transform.position, buttonUpPos.transform.position, pressSpeed);

    }
   

}
