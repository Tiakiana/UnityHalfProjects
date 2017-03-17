using UnityEngine;
using System.Collections;
using System;

public class WaterScr : MonoBehaviour, IResource {
    public ResourceType ResourceTyp
    {
        get;


        set;
    }

    public int Value
    {
        get;

        set;
    }

    public Vector3 Position { get; set; }

    // Use this for initialization
    void Start () {
        Position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
