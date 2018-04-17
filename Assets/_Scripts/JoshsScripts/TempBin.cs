using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBin : MonoBehaviour {

    public GameObject binUpper;
    public GameObject binLower;

    public GameObject obj1Pos1;
    public GameObject obj1Pos2;

    public GameObject obj2Pos1;
    public GameObject obj2Pos2;

    public ParticleSystem fire;

    public float OpenCloseSpeed;
    public float timeToWait;

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
          //  print("TimeCount is "+ timeCount);
        }
        else
            timeCount = 0f;

    }

    public void DoTheThing() {

        timeForThing = true;
        print("Open");
        binUpper.transform.position = Vector3.MoveTowards(obj1Pos1.transform.position, obj1Pos2.transform.position, OpenCloseSpeed);
        binLower.transform.position = Vector3.MoveTowards(obj2Pos1.transform.position, obj2Pos2.transform.position, OpenCloseSpeed);
        fire.gameObject.SetActive(true);

        if (timeCount >= timeToWait) {
            print("Close");
            binUpper.transform.position = Vector3.MoveTowards(obj1Pos2.transform.position, obj1Pos1.transform.position, OpenCloseSpeed);
            binLower.transform.position = Vector3.MoveTowards(obj2Pos2.transform.position, obj2Pos1.transform.position, OpenCloseSpeed);

            fire.gameObject.SetActive(false);
            timeForThing = false;
        }
    }


}
