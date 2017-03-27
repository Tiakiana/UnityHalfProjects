using UnityEngine;
using System.Collections;

public class ErfRotator : MonoBehaviour
{
    public float rotation;
	// Use this for initialization
	void Start ()
	{
	    EncounterController.EncCtrl.OnStep += Rotate;
	}

    public void Rotate()
    {
        transform.RotateAround(transform.right, rotation);


    }

    // Update is called once per frame
    void Update () {
	    if (Input.GetKeyDown("e"))
	    {
	        transform.RotateAround(transform.right,rotation);
	    }
	}
}
