using UnityEngine;
using System.Collections;
using UnityEditor;

public class globalFlock : MonoBehaviour {
    public GameObject fishPrefab;
    static int numFish = 10;
    public static int tankSize = 5;
    public static GameObject[] allFish = new GameObject[numFish];
    public static Vector3 goalPos = Vector3.zero;
    public GameObject goalPosMarker;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Random.Range(0,10000)<50)
	    {
	        goalPos = new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));
	        goalPosMarker.transform.position = goalPos;
	    }
	}
}
