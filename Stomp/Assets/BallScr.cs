using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I'm hit by player");

        if (collision.gameObject.tag == "Player")
        {

        GetComponent<Rigidbody>().AddForceAtPosition( collision.contacts[0].point - collision.transform.position*100,collision.contacts[0].point);
        GetComponent<Rigidbody>().AddForceAtPosition( collision.contacts[0].point - collision.transform.position*2,collision.contacts[0].point,ForceMode.Impulse);

        }

    }
}
