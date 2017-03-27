using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController GameCtrl;
    public List<CavemanStats> Stats = new List<CavemanStats>();
    public List<GameObject> Cavemen = new List<GameObject>();
    public bool NewGame;
    void Awake()
    {
        GameCtrl = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RemoveCaveman(GameObject cm)
    {
        Cavemen.Remove(cm);
    }

    public void BackToVillage()
    {

    }

    public void TranslateCavemen()
    {
        foreach (GameObject caveman in Cavemen)
        {
            Character car = caveman.GetComponent<Character>();
            CavemanStats cs = new CavemanStats();
            cs.Name = car.Name;
            cs.Age = car.Age;
            cs.Sex = car.Sex;
            cs.StatLine = car.GetStatLine();
            Stats.Add(cs);
        }
    }

    public void StartEncounterRun()
    {
        EncounterController.EncCtrl.StartNewWorldRun(Stats);
    }

    public void GoToEncounterLevel()
    {
        TranslateCavemen();
        SceneManager.LoadScene("Scene2Combat");
        // Ready player to press start encounters and shit. Which is S by the way.
    }

    
    public void AddCaveman(GameObject caveguy)
    {

        Cavemen.Add(caveguy);
    }

    void Start () {

    }
	
	void Update () {
	    if (Input.GetKeyDown("s"))
	    {
	        StartEncounterRun();
	    }
	}
}
