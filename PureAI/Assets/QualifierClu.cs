using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QualifierClu : MonoBehaviour {
    public List<GameObject> Scorers = new List<GameObject>();
    public float Score;
    public List<GameObject> ActionsList = new List<GameObject>();


	void Start () {
        Score = 0;
	}

    public void UseActions() {

        foreach (GameObject item in ActionsList)
        {
            item.GetComponent<IAction>().Execute();
        }

    }

    public float GetScore() {
        Score = 0;
        foreach (GameObject item in Scorers)
        {
            Score += item.GetComponent<IScorer>().ReturnScore();
        }
        return Score;
    }

	void Update () {
	
	}
}
