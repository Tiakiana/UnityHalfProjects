using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniMax : MonoBehaviour
{
    public int PlayerNumber = 2;
    public int MaxDepth;
    int BestMove = 9;
    int BestPawn = 9;
    float BestScore;

    int BestMoveOther = 9;
    int BestPawnOther = 9;
    float BestScoreOther;

    //Starter ballet
    //og returnerer det alderbedste move for spilleren
    public void MINIMAX()
    {
        BestMove = 9;
        BestPawn = 9;
        BestScore = -9999;
        for (int pawn = 0; pawn < 3; pawn++)
        {
            List<int> posibMoves = Board.BoardInst.GetValidMoves(Board.BoardInst.ConvertToBoardState(), PlayerNumber, pawn);

            for (int move = 0; move < posibMoves.Count; move++)
            {
                float score = MIN(Board.BoardInst.GetNewBoardStateShadow(Board.BoardInst.ConvertToBoardState(), PlayerNumber, pawn, posibMoves[move]), 0);
                if (score > BestScore)
                {
                    BestMove = move;
                    BestPawn = pawn;
                    BestScore = score;
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

        
        BestScore = -9999;
        float moveBestScore = -9999;
        for (int pawn = 0; pawn < 3; pawn++)
        {
            List<int> posibMoves = Board.BoardInst.GetValidMoves(Board.BoardInst.ConvertToBoardState(), PlayerNumber, pawn);

            for (int move = 0; move < posibMoves.Count; move++)
            {
                float score = MIN(Board.BoardInst.GetNewBoardStateShadow(boardState, PlayerNumber, pawn, posibMoves[move]), depth);
                if (score > BestScore)
                {
                    BestMove = move;
                    BestPawn = pawn;
                    BestScore = score;
                    moveBestScore = score;
                }


            }


        }
        return moveBestScore;
    }

    public float MIN(int[,,] boardState, int depth)
    {
        //if gameover return eval + ending
        //else if maxdepth return eval

        
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
            return EvaluateBoard(boardState,PlayerNumber);
        }

        BestScoreOther = 9999;
        float moveBestScore = 9999;
        for (int pawn = 0; pawn < 3; pawn++)
        {

            List<int> posibMoves = Board.BoardInst.GetValidMoves(boardState, otherPlayerNumber, pawn);

            for (int move = 0; move < posibMoves.Count; move++)
            {
                float score = MAX(Board.BoardInst.GetNewBoardStateShadow(Board.BoardInst.ConvertToBoardState(), otherPlayerNumber, pawn, posibMoves[move]), depth + 1);
                if (score < BestScore)
                {
                    BestMoveOther = move;
                    BestPawnOther = pawn;
                    BestScoreOther = score;
                    moveBestScore = score;
                }
            }


        }

        return moveBestScore;

    }

    

    public float EvaluateBoard(int[,,] boardState, int player)
    {
        float res = 1 + Random.Range(0.1f,1f);
        for (int pawn = 0; pawn < 3; pawn++)
        {
            res += 3 * Board.BoardInst.HowManyWillIKillShadow(boardState,player,pawn);
            if (Board.BoardInst.AmIThreatening(boardState, player, pawn))
            {
                res += 2;
            }
            
        }


        return res;
    }


}
