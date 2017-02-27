using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CoinScr : MonoBehaviour {
    public GameObject PanelWin;
    public AudioSource Music;
    public AudioClip musicWin;
    bool wining = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Coin")
        {
            if (!wining)
            {

                Debug.Log("Coin Drop");
                PanelWin.SetActive(true);
                Music.clip = musicWin;
                Music.Play();
                wining = true;
            }
      //      SceneManager.LoadScene(1);
        }
        

        
    }

}
