using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CanvasController : MonoBehaviour {
    public Text Points;
    // Use this for initialization
    public static CanvasController CvsCtrl;
    public int PlayerPoints = 0;
    public Sprite[] sprites = new Sprite[2];
    public Image selection, popUp;
    public Sprite[] PopUpSpritesDeath = new Sprite[5];
    public Sprite[] PopUpSpritesSpot = new Sprite[5];
    public Sprite[] PopUpSpritesKill = new Sprite[5];
    public AudioClip[] Dies = new AudioClip[0];
    public AudioClip[] Spot = new AudioClip[0];
    public AudioClip[] Kill = new AudioClip[0];
    bool ShowScreen = true;
    public GameObject youdeaded;
    public void YouDied() {
        youdeaded.SetActive(true);
        StartCoroutine("EndGame");
    }
    IEnumerator EndGame() {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);

    }

    public void SetBacon() {
        selection.sprite = sprites[0];

    }
    public void SetHat() {
        selection.sprite = sprites[1];
    }
    public void SetNothing() {
        selection.sprite = sprites[2];

    }
    public void SetSpottedAnimation() {
        if (ShowScreen)
        {
            GetComponent<AudioSource>().clip = Spot[Random.Range(0, Spot.Length)];
            GetComponent<AudioSource>().Play();
            
            popUp.sprite = PopUpSpritesSpot[Random.Range(0, PopUpSpritesSpot.Length)];
            popUp.SendMessage("StartEnter");
            StartCoroutine("CoolDown");


        }

    }
    public void SetKillAnimation()
    {
        if (ShowScreen)
        {
            GetComponent<AudioSource>().clip = Kill[Random.Range(0, Kill.Length)];
            GetComponent<AudioSource>().Play();
            popUp.sprite = PopUpSpritesKill[Random.Range(0, PopUpSpritesKill.Length)];
            popUp.SendMessage("StartEnter");
            StartCoroutine("CoolDown");

        }
    }
    public void SetDieAnimation()
    {
        GetComponent<AudioSource>().clip = Dies[Random.Range(0, Dies.Length)];
        GetComponent<AudioSource>().Play();


        popUp.sprite = PopUpSpritesDeath[Random.Range(0, PopUpSpritesDeath.Length)];
            popUp.SendMessage("StartEnter");
            StartCoroutine("CoolDown");
        
    }
    IEnumerator CoolDown() {
        ShowScreen = false;
        yield return new WaitForSeconds(7);
        ShowScreen = true;

    }
    void Awake() {

        CvsCtrl = this;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Points.text = "BACON GREASE: " + PlayerPoints;
        if (Input.GetKeyDown("p"))
        {
            SetSpottedAnimation();
        }
        if (Input.GetKeyDown("o"))
        {
            SetKillAnimation();
        }
        if (Input.GetKeyDown("i"))
        {
            SetDieAnimation();
        }
    }
}
