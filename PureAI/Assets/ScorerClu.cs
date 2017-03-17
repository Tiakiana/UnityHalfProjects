using UnityEngine;
using System.Collections;

public class ScorerClu : MonoBehaviour {
    public GameObject Context;
    ContextClu context;
	// Use this for initialization
	void Start () {
        context = Context.GetComponent<ContextClu>();
	}

    public float GetScore() {

        return context.Stats.Food;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
