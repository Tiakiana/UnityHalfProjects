using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour {
    public GameObject MetalTile;
    public static LevelScript LvlScr;

    public List<GameObject> Fighters = new List<GameObject>();
    void Awake() {
        LvlScr = this;

    }

	void Start () {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                Instantiate(MetalTile, new Vector3(x,y,1),Quaternion.identity);
            }
        }
	}

    public void UpgradeFighters() {
        foreach (GameObject item in Fighters)
        {
            item.GetComponent<FighterCtrl>().damage += 1;
            item.GetComponent<FighterCtrl>().StartBlinking();
        }

    }
	void Update () {
	
	}
}
