using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager GmInst;
    public int Player1Points, Player2Points;


    public Text Player1ScoreText, Player2ScoreText;
    public GameObject MovePanel;
    private MovePanelScr movePanelScr;
    public GameObject SquarePointedTo;
    public GameObject[] Player1Pawns = new GameObject[3];
    public GameObject[] Player2Pawns = new GameObject[3];
     Camera c;
    private bool isShowingMovePanel = false;


    void Awake()
    {
        GmInst = this;
    }

    void Start () {
        c = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        movePanelScr = MovePanel.GetComponent<MovePanelScr>();
    }

    void Update() {

        if (Input.GetKeyUp("w"))
        {
            
            PutPawnOnBoard(Player1Pawns[1].GetComponent<Pawn>());
        }

        if (Input.GetMouseButtonDown(1))
        {
            HideMovePanel();
        }
    }

    public void ShowMovePanel(GameObject go)
    {

        go.GetComponent<Pawn>().CheckForOptions();
        movePanelScr.SetOptions(go.GetComponent<Pawn>().Options);
        MovePanel.transform.position = c.WorldToScreenPoint(go.transform.position);

        MovePanel.SetActive(true);
        isShowingMovePanel = true;
        movePanelScr.pawn = go.GetComponent<Pawn>();


    }

    public void GetPointAndMove(bool player1)
    {
        if (player1)
        {
            Player1Points++;
            Player1ScoreText.text = "Player 1: " + Player1Points;
            //Move +1

        }
        else
        {
            Player2Points++;
            Player2ScoreText.text = "Player 2: " + Player2Points;

            //Move +1;
        }
    }

    public void HideMovePanel()
    {
        MovePanel.SetActive(false);
        isShowingMovePanel = false;
    }

    public void MovePiece()
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
                        GetPointAndMove(pawn.Player1Owned);
                  

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
                        GetPointAndMove(pawn.Player1Owned);



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
