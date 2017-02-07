using UnityEngine;
using System.Collections;

public class HumScanner {

    public float Sight;
    GameWorld gameworld;
    HumContext context;
    public HumScanner(GameWorld gw, HumContext con) {
        gameworld = gw;
        context = con;
    }


    public void ScanForResources(float playerpos) {
        foreach (IResource item in gameworld.resources)
        {

        }

    }



}
