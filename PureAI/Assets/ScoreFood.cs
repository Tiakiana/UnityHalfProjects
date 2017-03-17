using UnityEngine;
using System.Collections;

public class ScoreFood : MonoBehaviour, IScorer {

    public GameObject Context;
    ContextClu context;
    // Use this for initialization
    void Start()
    {
        context = Context.GetComponent<ContextClu>();
    }
    
    public float ReturnScore() {
        return context.Stats.Food;


    }


    void Update () {
	
	}
}
