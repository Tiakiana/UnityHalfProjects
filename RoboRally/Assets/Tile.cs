using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
    public Vector2 Position;
    public List<GameObject> Held;
    public GameObject Occupant;
    public bool Up,Right,Down,Left;
	void Start () {
	
	}
	public void SetPosition(int x, int y)
    {
        Position = new Vector2(x, y);
        
    }


	void Update () {
	
	}
}
