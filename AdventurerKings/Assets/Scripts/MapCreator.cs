using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {
    public List<Material> OverlandTileSprites = new List<Material>();

	// Use this for initialization
	void Start () {
        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
                go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                go.transform.position = new Vector3(x,y,0);
                go.transform.Rotate(Vector3.right, -90);
                go.AddComponent<Tile>();
                go.GetComponent<Tile>().SetTile(x,y,this,Random.Range(1,16),OverlandTileSprites[0]);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
