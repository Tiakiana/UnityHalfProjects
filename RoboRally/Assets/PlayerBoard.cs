using UnityEngine;
using System.Collections;

public class PlayerBoard : MonoBehaviour {
    public Card[] Registers = new Card[5];

    public void PlayCard(int i) {
        BroadcastMessage(Registers[i].Order);
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
