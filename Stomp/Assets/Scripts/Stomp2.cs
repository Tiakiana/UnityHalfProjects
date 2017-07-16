using UnityEngine;
using System.Collections;

public class Stomp2 : MonoBehaviour {

    public GameObject left;
    public GameObject Pickup;
    public bool isChild = true;
    public float spin;
    public string ActionKey;
    AudioSource ausource;
    // Use this for initialization
    void Start()
    {
        ausource = GetComponent<AudioSource>();
    }

    //Cube2.transform.parent=Cube1.transform

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(ActionKey))
        {
         //   ausource.Play();
            if (isChild)
            {
                isChild = false;
                transform.parent = null;
                left.transform.parent = transform;
              //  Pickup.transform.parent = transform;
            }
            else
            {
                isChild = true;
                left.transform.parent = null;
                transform.parent = left.transform;
            //    Pickup.transform.parent = left.transform;
            }
        }
        if (isChild)
        {

            transform.RotateAround(transform.parent.position, Vector3.up, Time.deltaTime * spin);
        }
        else
        {
         //   transform.LookAt(left.transform.position);
            left.transform.RotateAround(left.transform.parent.position, Vector3.up, Time.deltaTime * -spin);
        }

    }
}
