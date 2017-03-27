using UnityEngine;
using System.Collections;

public class SkyRotator : MonoBehaviour {
    public float rotation;
    // Use this for initialization
    void Start()
    {
        EncounterController.EncCtrl.OnStep += Rotate;
    }

    public void Rotate()
    {
        transform.RotateAround(transform.up, rotation);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            transform.RotateAround(transform.right, rotation);
        }
    }
}
