using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniMax : MonoBehaviour
{
    public int PlayerNumber = 2;
    public int MaxDepth;

    public Pawn[] AIPawns = new Pawn[3];
    public int[] PawnBestMove = new int[3];
    public bool IsPlayer1 = true;
    [SerializeField]
    private GameManager gm;
    private float alpha = float.MinValue, beta = float.MaxValue;
    int BestMove = 9;
    int BestPawn = 9;
    float BestScore;

    int BestMoveOther = 9;
    int BestPawnOther = 9;
    float BestScoreOther;
    float newbestScore;
    void Start()
    {
        StartCoroutine("waitforeverathing");
    }
    public void TakeTurn()
    {

        if (gm.player1sTurn == IsPlayer1)
        {

            MINIMAX();
       //     Debug.Log("Best score is " + BestScore);
            //Debug.Log("I choose the best pawn to be: " + BestPawn + "and best direction is: " + BestMove+ " With best score " + BestScore);
            MovePawn(BestPawn - 2, BestMove);


        }
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

    //Starter ballet
    //og returnerer det alderbedste move for spilleren
    public void MINIMAX()
    {
 //       Debug.Log("New Turn");
        BestMove = 9;
        BestPawn = 9;
        BestScore = -9999;
        for (int pawn = 2; pawn < 5; pawn++)
        {
            List<int> posibMoves = Board.BoardInst.GetValidMoves(Board.BoardInst.ConvertToBoardState(), PlayerNumber, pawn);
            //   Debug.Log(posibMoves[0] + " " + posibMoves[1] + " " + posibMoves[2]);
            string s = "Pawn number: " + pawn + " Best Moves Are: ";
            if (posibMoves.Count > 0)
            {
                for (int i = 0; i < posibMoves.Count; i++)
                {
                    s += posibMoves[i];
                }
                //Debug.Log(s);
                for (int move = 0; move < posibMoves.Count; move++)
                {
                    float minscore = MIN(Board.BoardInst.GetNewBoardStateShadow(Board.BoardInst.ConvertToBoardState(), PlayerNumber, pawn, posibMoves[move]), 0);
                   // Debug.Log("Score is " +score +" for pawn " +pawn);
                    if (minscore > BestScore)
                    {
                        BestMove = posibMoves[move];
                        BestPawn = pawn;
                        BestScore = minscore;
                    }
               //     Debug.Log("Current Best score is: " + BestScore);

                }
            }



        }



    }


    public float MAX(int[,,] boardState, int depth)
    {

        int otherPlayerNumber;
        if (PlayerNumber == 1)
        {
            otherPlayerNumber = 2;
        }
        else
        {
            otherPlayerNumber = 1;
        }
        if (depth == MaxDepth)
        {
            return EvaluateBoard(boardState, otherPlayerNumber);
        }


        //BestScore = -9999;
        //float moveBestScore = -9999;
        BestScoreOther = -9999;
        bool done = false;
    done:
        if (!done)
        {
            for (int pawn = 2; pawn < 5; pawn++)
            {
                List<int> posibMoves = Board.BoardInst.GetValidMoves(Board.BoardInst.ConvertToBoardState(), PlayerNumber, pawn);

                for (int move = 0; move < posibMoves.Count; move++)
                {



                    float Maxscore = MIN(Board.BoardInst.GetNewBoardStateShadow(boardState, PlayerNumber, pawn, posibMoves[move]), depth);
                    alpha = Maxscore;
                    if (beta >= alpha)
                    {
                        done = true;
                        goto done;
                    }
                    if (Maxscore >= BestScoreOther)
                    {
                        BestMoveOther = posibMoves[move];
                        BestPawnOther = pawn;
                        BestScoreOther = Maxscore;
                        //moveBestScore = score;
                    }


                }


            }
            //Debug.Log("Mananama" + BestScoreOther);
        }
        return BestScoreOther;
    }
    //Ser tingene fra AI'ens synspunkt
    public float MIN(int[,,] boardState, int depth)
    {
        //if gameover return eval + ending
        //else if maxdepth return eval
        int newbestMove;
        int newbestPawn;
        //for each other players legal move
        int otherPlayerNumber;
        if (PlayerNumber == 1)
        {
            otherPlayerNumber = 2;
        }
        else
        {
            otherPlayerNumber = 1;
        }
        if (depth == MaxDepth)
        {
            return EvaluateBoard(boardState, PlayerNumber);
        }

        //BestScoreOther = 9999;
        newbestScore = 9999;
        bool done = false;
    done:
        if (!done)
        {
            //culprit is moveBestScore

            for (int pawn = 2; pawn < 5; pawn++)
            {

                List<int> posibMoves = Board.BoardInst.GetValidMoves(boardState, otherPlayerNumber, pawn);

                for (int move = 0; move < posibMoves.Count; move++)
                {


                    float minscore = MAX(Board.BoardInst.GetNewBoardStateShadow(Board.BoardInst.ConvertToBoardState(), otherPlayerNumber, pawn, posibMoves[move]), depth + 1);
                    //Debug.Log("Pawn: " + pawn + ", Scores: " + newbestScore + "vs. " + score);
                    beta = minscore;
                    if (beta >= alpha)
                    {
                        done = true;
                        goto done;
                    }
                    if (minscore <= newbestScore)
                    {

                        newbestMove = posibMoves[move];
                        newbestPawn = pawn;
                        //bestScore = score;
                        newbestScore = minscore;
                        //Debug.Log("Pawn: " + pawn + ", Scores2: " + newbestScore  + "vs. " + score);
                    }

                }


            }
        }
        return newbestScore;

    }



    public float EvaluateBoard(int[,,] boardState, int player)
    {
        float res = 0 + Random.Range(0.1f, 1f);
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (boardState[x, y, 1] == player)
                {
                    res = res + 1;
                }
            }
        }

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (boardState[x, y, 1] != player)
                {
                    res = res - 1;
                }
            }
        }

        for (int pawn = 2; pawn < 5; pawn++)
        {
            res = res + 3 * Board.BoardInst.HowManyWillIKillShadow(boardState, player, pawn);
            if (Board.BoardInst.AmIThreatening(boardState, player, pawn))
            {
                res = res + 1;
            }

        }


        return res;
    }


}
