using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;
public class TestIphone : MonoBehaviour {
    public GameObject test;
    bool launched;
    Vector3 origin;
    Vector3 release;
    
	// Use this for initialization
	void Start () {
        launched = false;

	}
	
	// Update is called once per frame
	void Update () {
        int numtouch = Input.touchCount;
        if(numtouch!=0 && !launched)
        {
            launched = true;
            origin = Input.GetTouch(0).position;

        }
        else if(launched && numtouch!=0)
        {
            release = Input.GetTouch(0).position;

        }
        else if(launched &&numtouch==0)
        {
            Launch();
            launched = false;
            origin = Vector3.zero;
            release = Vector3.zero;
        }

    }
    void Launch()
    {
        test.GetComponent<Rigidbody>().AddForce(.0005f *-Vector3.Distance(origin,release) * (release - origin), ForceMode.Impulse);
    }
}
