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

    void Update() {

        if (Input.GetKeyUp("w"))
        {
            
            PutPawnOnBoard(Player1Pawns[1].GetComponent<Pawn>());
        }
    }

    public void ShowMovePanel(GameObject go)
    {
      


    }



    public void PutPawnOnBoard(Pawn pawn) {
        if (!pawn.OnBoard)
        {
            if (pawn.Player1Owned)
            {

                if (Board.BoardInst.Squares[2, 0].Occupant==null)
                {
                    pawn.MoveTo(new Vector3(2, 0, -2));
                    Board.BoardInst.Squares[2, 0].Occupant = pawn.gameObject;
                    pawn.OnBoard = true;
                }
                else
                {
                    if (!Board.BoardInst.Squares[2,0].Occupant.GetComponent<Pawn>().Player1Owned)
                    {

                        Board.BoardInst.Squares[2, 0].Occupant.GetComponent<Pawn>().BeatHome();
                        Player1Points++;
                  

                        Board.BoardInst.Squares[2, 0].Occupant = null;
                        pawn.MoveTo(new Vector3(2, 0, -2));
                        Board.BoardInst.Squares[2, 0].Occupant = pawn.gameObject;
                        pawn.OnBoard = true;
                    }
                    else
                    {
                        Debug.Log("Not Permitted move. There is already a black pawn in black starting area");
                    }
                }
            }
            //If he is owned by player two
            else
            {

                if (Board.BoardInst.Squares[2, 4].Occupant == null)
                {
                    pawn.MoveTo(new Vector3(2, 4, -2));
                    Board.BoardInst.Squares[2, 4].Occupant = pawn.gameObject;
                    pawn.OnBoard = true;
                }
                else
                {
                    if (Board.BoardInst.Squares[2, 4].Occupant.GetComponent<Pawn>().Player1Owned)
                    {
                        Board.BoardInst.Squares[2, 4].Occupant.GetComponent<Pawn>().BeatHome();
                        Player2Points++;
                       

                        Board.BoardInst.Squares[2, 4].Occupant = null;
                        pawn.MoveTo(new Vector3(2, 4, -2));
                        Board.BoardInst.Squares[2, 4].Occupant = pawn.gameObject;
                        pawn.OnBoard = true;
                    }
                    else
                    {
                        Debug.Log("Not Permitted move. There is already a white pawn in white starting area");

                    }
                }

                }
        } 

    }

}
