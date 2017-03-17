using UnityEngine;
using System.Collections;
using System;

class ThirstScorer : IScorer {
    public Context Context
    {
        get;


        set;
    }

    public float ReturnScore()
    {
        return 14;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
