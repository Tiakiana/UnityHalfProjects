using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {
    public AudioSource Audi;
	// Use this for initialization
	void Start () {
        Audi.time = Random.Range(0,Audi.clip.length);
        Audi.volume = 0;
        Audi.Play();
        StartCoroutine("TurnItUp");
	}

    IEnumerator TurnItUp() {
        while (Audi.volume<0.6f)
        {
            Audi.volume += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
