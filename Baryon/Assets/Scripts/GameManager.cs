using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager GmInst;
    public int Player1Points, Player2Points;

    public GameObject MovePanel;
    public GameObject SquarePointedTo;
    public GameObject[] Player1Pawns = new GameObject[3];
    public GameObject[] Player2Pawns = new GameObject[3];


    void Awake()
    {
        GmInst = this;
    }

    void Start () {
	    
	}

    public void ShowMovePanel(GameObject go)
    {
      


    }



}
