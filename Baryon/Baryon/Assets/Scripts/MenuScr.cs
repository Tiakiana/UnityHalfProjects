using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MenuScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetAIPlay()
    {
        PlayerPrefs.SetInt("OnePlayerGame",1);
        SceneManager.LoadScene(1);
    }
    public void SetAIPlay(int AILevel)
    {
        PlayerPrefs.SetInt("OnePlayerGame", 1);
        PlayerPrefs.SetInt("AILevel", AILevel);
        SceneManager.LoadScene(1);
        
    }

    public void Set2PlayerGame()
    {
        PlayerPrefs.SetInt("OnePlayerGame", 0);
        SceneManager.LoadScene(1);
    }

}
