using UnityEngine;
using System.Collections;

public class NavmessingAround : MonoBehaviour
{
    public GameObject go;
	// Use this for initialization
	void Start ()
	{
	    GetComponent<NavMeshAgent>().SetDestination(go.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
