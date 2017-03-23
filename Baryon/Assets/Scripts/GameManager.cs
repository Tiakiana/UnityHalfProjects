using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditorInternal;

public class GameManager : MonoBehaviour
{
    private Camera c;
    private MovePanelScr movePanelScr;
    private int Player1Points, Player2Points;

    [Header("PlayModes")]
    public bool TwoPlayerMode = false;
    public bool ZeroPlayerMode = false;
    public bool OnePlayerMode = false;
    public bool GameMasterMode = false;
    public static GameManager GmInst;

     bool EndOfGame = false;

    [Header("References")]

    public Text Player1ScoreText, Player2ScoreText;
    public GameObject MovePanel;
    public GameObject[] Player1Pawns = new GameObject[3];
    public GameObject[] Player2Pawns = new GameObject[3];

    [HideInInspector]
    public bool TurnDone;
    [HideInInspector]
    public bool player1sTurn = true;
    bool player1Won = false;
    bool player2Won = false;
    [Header("Moves")]
    public int Moves = 0;



    void Awake()
    {
        GmInst = this;
    }

    void Start () {
        c = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        movePanelScr = MovePanel.GetComponent<MovePanelScr>();
    }

    void Update() {

        if (Input.GetKeyUp("s"))
        {
             StartCoroutine("PlayGame");

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
    //    isShowingMovePanel = true;
        movePanelScr.pawn = go.GetComponent<Pawn>();


    }

    
    public void HideMovePanel()
    {
        MovePanel.SetActive(false);
      // isShowingMovePanel = false;
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
                    Moves--;
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
                        Moves--;
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
                    Moves--;
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
                        Moves--;
                    }
                    else
                    {
                        Debug.Log("Not Permitted move. There is already a white pawn in white starting area");

                    }
                }

                }
        } 

    }
    public void GetPointAndMove(bool player1)
    {
        if (player1)
        {
            Player1Points++;
            Player1ScoreText.text = "Player 1: " + Player1Points;
            Moves++;

        }
        else
        {
            Player2Points++;
            Player2ScoreText.text = "Player 2: " + Player2Points;
            Moves++;
        }

        if (Player1Points >= 5)
        {
            // Player eins hat gewonnen!
            EndOfGame = true;

        }
        if (Player2Points >= 5)
        {
            // Player Zwei hat gewonnen!
            EndOfGame = true;
        }
    }

    void CleanUp()
    {
        player1sTurn = true;
        player1Won = false;
        player2Won = false;
        //clean up

        //nulstille brættet
        Board.BoardInst.CleanUpBoard();

        // nulstille brikker

        foreach (GameObject item in Player1Pawns)
        {
            item.GetComponent<Pawn>().BeatHome();
        }
        foreach (GameObject item in Player2Pawns)
        {
            item.GetComponent<Pawn>().BeatHome();
        }
        //nulstille points
        Player1Points = 0;
        Player2Points = 0;
        Player1ScoreText.text = "Player 1: ";
        Player2ScoreText.text = "Player 2: ";


    }

    public IEnumerator TakeTurn()
    {
        Debug.Log("Starting a turn");
        Moves = 1;
        while (!TurnDone)
        {
            if (Moves <=0)
            {
                TurnDone = true;
                break;
                
            }
            yield return null;
        }
        foreach (GameObject item in Player1Pawns)
        {
            item.GetComponent<Pawn>().ResetLastPosition();
        }
        foreach (GameObject item in Player2Pawns)
        {
            item.GetComponent<Pawn>().ResetLastPosition();
        }
        if (player1sTurn)
        {
            player1sTurn = false;
        }
        else
        {
            player1sTurn = true;
        }

    }

    public IEnumerator PlayGame()
    {
        Debug.Log("Starting game");
        CleanUp();

        // Take the turn

        while (!player1Won && !player2Won)
        {
            int moves = 1;
            ExtraMove:


            //Checks constantly for win
            if (Player1Points>4)
            {
                player1Won = true;
                TurnDone = true;
                Debug.Log("And the grueling battle ended with the victory of Player 1");

            }
            else if (Player2Points > 4)
            {
                player2Won = true;
                TurnDone = true;
                Debug.Log("And the grueling battle ended with the victory of Player 2");
            }
           else if (TurnDone)
            {
                TurnDone = false;
                StartCoroutine("TakeTurn");

            }

            yield return null;
        }
        StartCoroutine("PlayGame");

    }


}
