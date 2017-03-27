using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Encounter : MonoBehaviour
{
    

    public enum EncounterType
    {
        Combat = 1,
        Hazard,
        Rendezvous,
        EndRun
    }

    public int Step;
    public EncounterType Type;
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> EnemiesToSpawn = new List<GameObject>();

    public void UpdatePos()
    {
        if (Enemies.Count > 0)
        {
            float thing = 0;
            foreach (GameObject enemy in Enemies)
            {
                enemy.transform.position = EncounterController.EncCtrl.StepPositions[Step+5].transform.position;
                enemy.transform.position += Vector3.right * 0.8f * thing;
                thing += 0.7f;
            }
        }
    }

    public void RemoveEnemy(GameObject me)
    {
        Enemies.Remove(me);
    }

    public void SpawnAll()
    {
        if (EnemiesToSpawn.Count>0)
        {
            foreach (GameObject item in EnemiesToSpawn)
            {
                Enemies.Add(Instantiate(item, Vector3.down, Quaternion.identity) as GameObject);

            }
        }
        if (Enemies.Count > 0)
        {
            foreach (GameObject enemy in Enemies)
            {
                enemy.GetComponent<EnemyScr>().SetEncounter(this);
            }
        }
    }

    void Start()
    {
        EncounterController.EncCtrl.OnStep += UpdatePos;
        SpawnAll();
    }

}
