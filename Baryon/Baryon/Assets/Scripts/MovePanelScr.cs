using UnityEngine;
using System.Collections;

public class MovePanelScr : MonoBehaviour
{
    public Pawn pawn;
    public GameObject[] buttons = new GameObject[5];
	// Use this for initialization
	void Start () {
	
	}

    public void SetOptions(bool[] options)
    {
        for (int i = 0; i < 5; i++)
        {
            buttons[i].SetActive(options[i]);
        }
    }

    public void MoveUp()
    {
        pawn.Move(Pawn.Directionale.Up);
    }
    public void MoveDown()
    {
        pawn.Move(Pawn.Directionale.Down);
    }
    public void MoveRight()
    {
        pawn.Move(Pawn.Directionale.Right);
    }
    public void MoveLeft()
    {
        pawn.Move(Pawn.Directionale.Left);
    }

    public void PutOnBoard()
    {
        GameManager.GmInst.PutPawnOnBoard(pawn);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
