using UnityEngine;
using System.Collections;
using System.Threading;

public class Connectingy : MonoBehaviour
{
    private AsyncOperation async;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Listener()
    {
        while (true)
        {
            yield return async;
        }
    }

}
