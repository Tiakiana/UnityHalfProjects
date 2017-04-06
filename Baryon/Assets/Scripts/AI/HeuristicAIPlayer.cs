using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class HeuristicAIPlayer : MonoBehaviour
{
    public Pawn[] AIPawns = new Pawn[3];
    public int[] PawnBestMove= new int[3];
    public bool IsPlayer1 = true;
    [SerializeField]
    private GameManager gm;
    int bestMove = -1;

    // Use this for initialization
    void Start()
    {/*
        gm = GameManager.GmInst;

        if (IsPlayer1)
        {
            for (int i = 0; i < 3; i++)
            {
                AIPawns[i] = gm.Player1Pawns[i].GetComponent<Pawn>();
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                AIPawns[i] = gm.Player2Pawns[i].GetComponent<Pawn>();
            }
        }
        */
        StartCoroutine("waitforeverathing");
    }
    // skal kalkulere hvad det bedste move er for denne brik.


    private float Heuristic(Pawn pawn)
    {
        float res = 0;
        bestMove = -1;
        //Get all the options and make a list of integers that can be interpreted as moves
        pawn.CheckForOptions();
        List<int> options = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            if (pawn.Options[i])
            {
                int x = i;
                options.Add(x);
            }
        }

        float bestScore = 0;
        for (int i = 0; i < options.Count; i++)
        {

            Square squareExamined = Pawn.GetSquareInDirection(pawn, Pawn.ConvertIntToDir(options[i]));
            if (squareExamined.SQColour == pawn.Colour)
            {
                float score = (Board.BoardInst.HowManyWillIKill(pawn.Player1Owned, squareExamined.SQColour)) * 3 + (Random.Range(0.1f, 1));
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = options[i];
                }
            }
            if (Board.BoardInst.IsNextToColour(pawn.Colour, (int)pawn.transform.position.x, (int)pawn.transform.position.y))
            {
                float score = 2f + (float)(Random.Range(0.1f,1));
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = options[i];
                }
            }
            if (options[i] == 4)
            {
                float score = 2.1f + Random.Range(0.1f, 1);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = options[i];
                }
            }
            else
            {
                float score = 1f + Random.Range(0.1f, 1f);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = options[i];
                }
            }

        }

      //  Debug.Log("For Pawn: " + pawn.gameObject.name + " " + bestMove + " is the best move");
        res = bestScore;
        return res;
    }
    private float Heuristic(Square[,] boardstate, Pawn pawn)
    {
        float res = 0;
        bestMove = -1;
        //Get all the options and make a list of integers that can be interpreted as moves
        pawn.CheckForOptions();
        List<int> options = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            if (pawn.Options[i])
            {
                int x = i;
                options.Add(x);
            }
        }

        float bestScore = 0;
        for (int i = 0; i < options.Count; i++)
        {

            Square squareExamined = Pawn.GetSquareInDirection(pawn, Pawn.ConvertIntToDir(options[i]));
            if (squareExamined.SQColour == pawn.Colour)
            {
                float score = (Board.BoardInst.HowManyWillIKill(pawn.Player1Owned, squareExamined.SQColour)) * 3 + (Random.Range(0.1f, 1));
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = options[i];
                }
            }
            if (Board.BoardInst.IsNextToColour(pawn.Colour, (int)pawn.transform.position.x, (int)pawn.transform.position.y))
            {
                float score = 2f + (float)(Random.Range(0.1f, 1));
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = options[i];
                }
            }
            if (options[i] == 4)
            {
                float score = 2.1f + Random.Range(0.1f, 1);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = options[i];
                }
            }
            else
            {
                float score = 1f + Random.Range(0.1f, 1f);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = options[i];
                }
            }

        }

        Debug.Log("For Pawn: " + pawn.gameObject.name + " " + bestMove + " is the best move");
        res = bestScore;
        return res;
    }
    public IEnumerator waitforeverathing()
    {
        yield return new WaitForSeconds(0.5f);
        //    gm = GameManager.GmInst;

        if (IsPlayer1)
        {
            for (int i = 0; i < 3; i++)
            {
                AIPawns[i] = gm.Player1Pawns[i].GetComponent<Pawn>();
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                AIPawns[i] = gm.Player2Pawns[i].GetComponent<Pawn>();
            }
        }
    }

    public void TakeTurn()
    {
     
        if (gm.player1sTurn == IsPlayer1)
        {


            bm = CalcbestMove();
            MovePawn(bm.Pawn, bm.Direction);


        }
    }

    public void MovePawn(int pawn, int direction)
    {

        switch (direction)
        {
            case 0:
                AIPawns[pawn].Move(Pawn.Directionale.Left);
                break;
            case 1:
                AIPawns[pawn].Move(Pawn.Directionale.Up);
                break;
            case 2:
                AIPawns[pawn].Move(Pawn.Directionale.Right);
                break;
            case 3:
                AIPawns[pawn].Move(Pawn.Directionale.Down);
                break;
            case 4:
                gm.PutPawnOnBoard(AIPawns[pawn]);
                break;

            default:
                Debug.Log("No Valid move from Heuristic Player AI");
                break;
        }

    }
    public void MovePawn(Pawn pawn, int direction)
    {

        switch (direction)
        {
            case 0:
                pawn.Move(Pawn.Directionale.Left);
                break;
            case 1:
                pawn.Move(Pawn.Directionale.Up);
                break;
            case 2:
                pawn.Move(Pawn.Directionale.Right);
                break;
            case 3:
                pawn.Move(Pawn.Directionale.Down);
                break;
            case 4:
                gm.PutPawnOnBoard(pawn);
                break;

            default:
                Debug.Log("No Valid move from Heuristic Player AI");
                break;
        }

    }

    public class BestMove
    {
        public Pawn Pawn;
        public int Direction;
       public float BestScore;

        public BestMove(Pawn pawn, float bestScore, int direction )
        {
           Pawn = pawn;

            BestScore = bestScore;
            Direction = direction;
        }
}
BestMove bm;
public BestMove CalcbestMove()
{
        List<BestMove> bestMoves = new List<BestMove>();
    
    BestMove bmPawn0 = new BestMove(AIPawns[0], Heuristic(AIPawns[0]), bestMove);
    BestMove bmPawn1 = new BestMove(AIPawns[1], Heuristic(AIPawns[1]), bestMove);
    BestMove bmPawn2 = new BestMove(AIPawns[2], Heuristic(AIPawns[2]), bestMove);

        bestMoves.Add(bmPawn0);
        bestMoves.Add(bmPawn1);
        bestMoves.Add(bmPawn2);

        List<BestMove> SortedList = bestMoves.OrderBy(o => o.BestScore).ToList();
        SortedList.Reverse();
       // Debug.Log(bestMoves[0].Direction +" " + bestMoves[1].Direction + " "+ bestMoves[2].Direction);

        return SortedList[0];

}
    public float AnalyzeBoardState(Vector2[] boardState, bool player1) {
        float res = 0;
        return res;


    }

// Update is called once per frame
void Update()
{
    if (Input.GetKeyDown("c"))
    {
        Heuristic(AIPawns[0]);
        Heuristic(AIPawns[1]);
        Heuristic(AIPawns[2]);
            bm = CalcbestMove();
          //  Debug.Log(bm.Pawn.gameObject.name + ": Best Move is: " + bm.Direction + " With score " + bm.BestScore);

    }

}



}
