using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameWorld : MonoBehaviour {

    public List<IMonster> monsters = new List<IMonster>();
    public List<IResource> resources = new List<IResource>();




    void Start() {

        IResource gold = new IResource(-10, 10);

        resources.Add(gold);
        IMonster monter = new IMonster(5, 5);
        monsters.Add(monter);


    }


}
