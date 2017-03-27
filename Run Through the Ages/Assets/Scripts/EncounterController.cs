using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EncounterController : MonoBehaviour
{
    public int GroupStep;
    public static EncounterController EncCtrl;

    public List<CavemanStats> CavemanStatistics = new List<CavemanStats>();
    public List<GameObject> CavemanPositions = new List<GameObject>();

    public List<GameObject> Cavemen = new List<GameObject>();
    public List<Encounter> Encounters = new List<Encounter>();
    public float WaitFor;
    private Encounter crntEncounter;
    public bool WorldRunning;
    public bool Moving;
    public List<GameObject> StepPositions = new List<GameObject>();
    List<GameObject> FledTheBattle = new List<GameObject>();
    //Det her burder være i et creator pattern for sig selv. I Know What I do here is stupid.

    public GameObject CavemanPrefab;

    void Awake()
    {
        EncCtrl = this;

    }

    void Start ()
    {
        CombatController.ComCtrl.OnEndCombat += EndOfEncounter;

    }

    public void StartNewWorldRun(List<CavemanStats> cavemenToInstantiate )
    {
        
        Cavemen.Clear();
        CavemanStatistics.Clear();
        CavemanStatistics = cavemenToInstantiate;
        for (int i = 0; i < CavemanStatistics.Count; i++)
        {
            Cavemen.Add(CreateCaveman(CavemanStatistics[i].Name, CavemanStatistics[i].Age, CavemanStatistics[i].Sex, CavemanStatistics[i].StatLine, CavemanPositions[i].gameObject.transform.position));
        }

        StartCoroutine("WorldRun");
    }


	void Update () {
	
	}

    public GameObject CreateCaveman(string name, int age, bool sex, float[] stats, Vector3 place)
    {
        GameObject newCaveman = Instantiate(CavemanPrefab, Vector3.down, Quaternion.identity) as GameObject;
        //public void CreateCharacter(string name, int age, bool sex, float[] statline)
        newCaveman.GetComponent<Character>().CreateCharacter(name,age,sex,stats);
        newCaveman.transform.position = place;
        return newCaveman;

    }

    public void CreateCaveman(Character stats, Vector3 place)
    {
        GameObject newCaveman = Instantiate(CavemanPrefab, Vector3.down, Quaternion.identity) as GameObject;
        newCaveman.transform.position = place;
      
    }



    public delegate void Jump();

    public event Jump OnStep;

    public void Step()
    {
        OnStep();
        GroupStep++;
        bool startencounter = false;
        foreach (Encounter item in Encounters)
        {
            if (GroupStep == item.Step)
            {
                crntEncounter = item;
                startencounter = true;
            }

        }
        if (startencounter)
        {
            StartEncounter();
        }



    }

    public void EndOfEncounter()
    {

        if (Cavemen.Count>0)
        {
            if (FledTheBattle.Count>0)
            {
                foreach (GameObject go in FledTheBattle)
                {
                        go.transform.localScale = new Vector3(0.6324333f, 0.6324332f, 0.7905414f);

                }
                FledTheBattle.Clear();
            }
        Moving = true;

        }
        else
        {
            Debug.Log("End of game. You are of deadest");
        }
    }


    public void StartEncounter()
    {
       //check for type of encounter
       // handle Apropriately
       //??????
       // Profit

        switch (crntEncounter.Type)
        {
                case Encounter.EncounterType.Combat:
                Moving = false;

                List<GameObject> Fighters = new List<GameObject>();
                List<GameObject> Fleeing = new List<GameObject>();

                foreach (GameObject caveman in Cavemen)
                {
                    
                    int x = UnityEngine.Random.Range(1, 101);
                    if (caveman.GetComponent<Character>().GetCourage()< x)
                    {
                        Fleeing.Add(caveman);
                        caveman.transform.localScale = new Vector3(-0.6324333f, 0.6324332f, 0.7905414f);
                        AudioController.AudioCtrl.PlayArgh();
                    }
                    else
                    {
                        Fighters.Add(caveman);
                    }
                    
                    

                }

                if (Fighters.Count>0)
                {
                    foreach (GameObject enemy in crntEncounter.Enemies)
                    {
                        Fighters.Add(enemy);
                    }
                    FledTheBattle = Fleeing;
                    CombatController.ComCtrl.SetCombatantsAndStartCombat(Fighters);

                }
                




                break;

                case Encounter.EncounterType.Hazard:

                break;

                case Encounter.EncounterType.EndRun:
                // Go to next screen
                WorldRunning = false;
                Debug.Log("Reached the end of destination");
                SceneManager.LoadScene("Scene1");
                break;

            default:
                break;
        }


    }

    public void RemoveMe(GameObject go)
    {
        Cavemen.Remove(go);
    }

    public IEnumerator WorldRun()
    {
        WorldRunning = true;
        Moving = true;
        while (WorldRunning)
        {
            yield return new WaitForSeconds(WaitFor);
            if (Moving)
            {
            Step();

            }

        }

    }

}
