using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//using System;

public class GameController : MonoBehaviour {
    public Text ChoiceA;
    public Text ChoiceB;
    public Text ChoiceC;

    public Text NexusText;
    public Image Image;
    public GameObject CurrentNexus;

    public GameObject[] Nexi = new GameObject[5];
    public void LoadNexi() {
        List<GameObject> gos = new List<GameObject>(Nexi);
        int x = 0;
        x = Random.Range(0,gos.Count);
        CurrentNexus = gos[x];
        gos.RemoveAt(x);
                 
        for (int i = 0; i < Nexi.Length; i++)
        {

            x = Random.Range(0,Nexi.Length);

        }
    }

	void Start () {
        ChoiceA.text = "Thomas er sej";
        

	}
	
	void Update () {
	
	}
}
