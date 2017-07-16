using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
    // List markets
    public List<string> TerrainFeatures = new List<string>();

    

    public List<Market> Markets = new List<Market>();
    public List<Tile> Neighbours = new List<Tile>();
    public float Xpos, Ypos;
    public MapCreator mapCreator;
    public Market.TerrainTypes Terrain;

    public void SetTile(float xpos, float ypos, MapCreator mapcreator, int terrainType, Material texture) {
        Xpos = xpos;
        Ypos = ypos;
        mapCreator = mapcreator;
        Terrain = (Market.TerrainTypes)terrainType;
        GetComponent<MeshRenderer>().material = texture;

    }
    void Start () {
	
	}


	
	void Update () {
	
	}
}
