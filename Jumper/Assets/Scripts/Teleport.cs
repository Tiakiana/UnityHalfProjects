using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Teleport : NetworkBehaviour {
    public float Range;
    public Vector3 Target, StartMarker;
    public GameObject FloatTarget;
    public GameObject Camera;
    public float time = 0.5f;
	// Use this for initialization
	void Start () {
      

    }

    // Update is called once per frame
    void Update () {
       // Debug.Log("Is local:"+ isLocalPlayer);

        if (!isLocalPlayer)
            return;

        RaycastHit hit;

      

        if (Input.GetMouseButton(1))
        {

            if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, Range))
            {

                GetComponent<LineRenderer>().SetPosition(0, transform.position +new Vector3(0,2,0));
                GetComponent<LineRenderer>().SetPosition(1, hit.point);
                StartMarker = transform.position;
                Target = hit.point;

            }


        }
        if (Input.GetMouseButtonUp(1))
        {
            StopCoroutine("Floating");
            GameObject go = new GameObject();
            go.transform.rotation = transform.transform.rotation;
            go.transform.position = Target;
            go.transform.position -= go.transform.forward *6;
            FloatTarget = go;
            Destroy(go, 5);
            StartCoroutine("Floating");
        }
    }

    IEnumerator Floating() {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(transform.position, FloatTarget.transform.position);
        while (true)
        {
            float distCovered = (Time.time - startTime) * time;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(StartMarker, FloatTarget.transform.position, fracJourney);
            yield return null;
        }

    }
}
