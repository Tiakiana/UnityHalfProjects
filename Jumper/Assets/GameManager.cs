using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager GMInst;
    // Use this for initialization
    public GameObject PanelOfDeath;
    void Awake() {

        GMInst = this;
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
