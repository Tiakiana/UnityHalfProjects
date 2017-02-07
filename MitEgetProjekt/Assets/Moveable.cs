using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {
    public GameObject target;
    public NavMeshAgent nma;
	// Use this for initialization
	void Start () {
        nma = GetComponent<NavMeshAgent>();
    //    nma.SetDestination(target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
