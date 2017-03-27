using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    public static AudioController AudioCtrl;
    private AudioSource audi;
    private AudioSource audi2;
    private bool AndaoneAndatwo = false;
    public List<AudioClip> Audios = new List<AudioClip>();

    void Awake()
    {
        AudioCtrl = this;
    }

    void Start ()
    {
        audi = GetComponent<AudioSource>();
        audi2 = transform.GetComponentInChildren<AudioSource>();
        EncounterController.EncCtrl.OnStep += PlayHop;

    }

    public void PlayHop()
    {
        audi.clip = Audios[1];
        if (AndaoneAndatwo)
        {
            audi.pitch = Random.Range(0.8f, 1f);
            audi.Play();
            AndaoneAndatwo = false;
        }
        else
        {
            audi2.pitch = Random.Range(0.8f, 1f);
            audi2.Play();
            AndaoneAndatwo = true;
        }
    }

    public void PlayArgh()
    {
        audi.clip = Audios[0];
        audi.pitch = 1;
        audi.Play();
    }


    // Update is called once per frame
    void Update () {
	    if (Input.GetKeyDown("e") || Input.GetKeyDown("w"))
	    {
	        if (AndaoneAndatwo)
	        {
                audi.pitch = Random.Range(0.8f, 1f);
                audi.Play();
	            AndaoneAndatwo = false;
	        }
	        else
	        {
                audi2.pitch = Random.Range(0.8f, 1f);
                audi2.Play();
	            AndaoneAndatwo = true;
	        }
	    }
	}
}
