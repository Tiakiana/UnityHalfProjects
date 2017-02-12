using UnityEngine;
using System.Collections;

public class TestyTheTested : MonoBehaviour
{
    private AsyncOperation async;
	// Use this for initialization
	void Start ()
	{
	    StartCoroutine("testy");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    IEnumerator testy()
    {
        while (true)
        {
        yield return async;

        }

    }
}
