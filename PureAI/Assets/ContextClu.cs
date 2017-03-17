using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContextClu : MonoBehaviour {
   // public float DefaultQualifierThreshhold { get; set; }

    public void DefaultAction()
    {

    }
    List<GameObject> resources = new List<GameObject>();
    public Stats Stats;

	// Use this for initialization
	void Start () {
        Stats = GetComponent<Stats>();
	}
	



	// Update is called once per frame
	void Update () {
	
	}
}
