using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
    public float Range;
    public Vector3 Target, StartMarker;
    public GameObject FloatTarget;
    public float time = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Range))
        {

            GetComponent<LineRenderer>().SetPosition(0, transform.parent.transform.position);
            GetComponent<LineRenderer>().SetPosition(1, hit.point);
            StartMarker = transform.parent.position;
            Target = hit.point;

        }


        if (Input.GetMouseButton(1))
        {
            StopCoroutine("Floating");

         //   Debug.Log("Fyrst igår");
           // transform.parent.position = Target;

            GameObject go = new GameObject();
            go.transform.rotation = transform.parent.transform.rotation;
            go.transform.position = Target;
            go.transform.position -= go.transform.forward;
            FloatTarget = go;
            Destroy(go, 5);

            StartCoroutine("Floating");
            
            //transform.parent.transform.eulerAngles = vec;

        }
        if (Input.GetMouseButtonUp(1))
        {

        }
    }

    IEnumerator Floating() {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(transform.parent.position, FloatTarget.transform.position);
        while (true)
        {
            float distCovered = (Time.time - startTime) * time;
            float fracJourney = distCovered / journeyLength;
            transform.parent.position = Vector3.Lerp(StartMarker, FloatTarget.transform.position, fracJourney);
            yield return null;
        }

    }
}
