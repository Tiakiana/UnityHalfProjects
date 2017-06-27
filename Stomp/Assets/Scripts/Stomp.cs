using UnityEngine;
using System.Collections;

public class Stomp : MonoBehaviour {
    public GameObject left;
    public GameObject right;
    public bool isChild = true;
    public float spin;
    // Use this for initialization
	void Start () {
	
	}

    //Cube2.transform.parent=Cube1.transform

    // Update is called once per frame
    void Update () {
     
        
        if (Input.GetKeyDown("g"))
        {
            if (isChild)
            {
                isChild = false;
                transform.parent = null;
                left.transform.parent = transform;
            }
            else
            {
                isChild = true;
                left.transform.parent = null;
                transform.parent = left.transform;
            }
        }
        if (isChild)
        {
            transform.RotateAround(transform.parent.position, Vector3.up, Time.deltaTime * spin);
        }
        else
        {
            left.transform.RotateAround(left.transform.parent.position, Vector3.up, Time.deltaTime * spin);
        }

    }
}
