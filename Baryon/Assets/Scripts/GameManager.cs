using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager GmInst;
    public GameObject MovePanel;

    void Awake()
    {
        GmInst = this;
    }

    void Start () {
	    
	}

    public void ShowMovePanel(GameObject go)
    {
        MovePanel.SetActive(true);


    }



}
