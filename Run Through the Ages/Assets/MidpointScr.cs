using UnityEngine;
using System.Collections;

public class MidpointScr : MonoBehaviour {

    public float rotation;
    public float StepsInWorld;
    public GameObject Erf;
    public GameObject PlaceHere;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < StepsInWorld; i++)
        {
            GameObject go = new GameObject();
            go.transform.position = PlaceHere.transform.position;
            go.transform.SetParent(Erf.transform);
            go.name = "Step #" + i;
            EncounterController.EncCtrl.StepPositions.Add(go);
     

            Rotate();
        }
    }

    public void Rotate()
    {
        transform.RotateAround(transform.right, rotation);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = PlaceHere.transform.TransformPoint(PlaceHere.transform.position);
            Rotate();
        }
    }
}
