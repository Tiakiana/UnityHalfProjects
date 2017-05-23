﻿using UnityEngine;
using System.Collections;
//using System;
using System.Collections.Generic;

public class MiniMax2 : MonoBehaviour
{

    public int PlayerNumber = 2;
    private int otherPlayerNumber;
    // int[,,] tempBoardstate = new int[5, 5, 3];
    private Move best;
    private Move myBest;
    public int maxDepth = 3;
   // public int currentDepth = 0;

    //UnitySpecifik:
    public GameManager gm;
    public bool IsPlayer1;
    public Pawn[] AIPawns = new Pawn[3];

    public class Node
    {
        public Node ParentNode;
        public List<Node> ChildrenNodes;
        public int Player;
        public Move move;

        public Node()
        {
            ChildrenNodes = new List<Node>();

        }
    }

    public void MINIMAX(int[,,] boardState)
    {
        best = new Move(-9999, 9, 9);
        myBest = new Move(-9999, 9, 9);

        Node root = new Node();
        for (int pawn = 2; pawn < 5; pawn++)
        {



            List<int> posibMoves = Board.BoardInst.GetValidMoves(boardState, PlayerNumber, pawn);


            string s = " ";
            for (int i = 0; i < posibMoves.Count; i++)
            {
                s += posibMoves[i];
            }
            //      Debug.Log("The moves are " + s + " For pawn: " + pawn  + "    Player: " + PlayerNumber);
            for (int posiMove = 0; posiMove < posibMoves.Count; posiMove++)
            {
                Move m = new Move(-9999, pawn, posibMoves[posiMove]);
                // tempBoardstate = Board.BoardInst.GetNewBoardStateShadow(boardState, PlayerNumber, pawn, posibMoves[posiMove]);

                Node n = new Node();
                n.ParentNode = root;
                n.move = m;
                n.Player = PlayerNumber;
                n.ParentNode.ChildrenNodes.Add(n);

                m.score = MIN(Board.BoardInst.GetNewBoardStateShadow(boardState, otherPlayerNumber, pawn, posibMoves[posiMove]), n, 0); //+ EVAL(boardState, PlayerNumber,pawn);

                if (m.score >= myBest.score)
                {
              //      Debug.Log("Pawn: " + pawn + " direction: " + m.direction + "Score is: " + m.score);
                    myBest = m.Clone() as Move;
                }

            }
        }
       // Debug.Log("Done");

    }
    public float MIN(int[,,] boardState, Node node, int currentDepth)
    {
        //  if (maxDepth == currentDepth)
        //   {
        //   return EVAL(Board.BoardInst.ConvertToBoardState(), otherPlayerNumber);
        //   }
        //   else
        // {

        for (int pawn = 2; pawn < 5; pawn++)
        {
            List<int> posibMoves = Board.BoardInst.GetValidMoves(Board.BoardInst.ConvertToBoardState(), otherPlayerNumber, pawn);
            for (int posiMove = 0; posiMove < posibMoves.Count; posiMove++)
            {
                best.score = 9999;

                Move m = new Move(9999, pawn, posibMoves[posiMove]);

                Node n = new Node();
                n.ParentNode = node;
                n.move = m;
                n.Player = otherPlayerNumber;
                n.ParentNode.ChildrenNodes.Add(n);

                //tempBoardstate = Board.BoardInst.GetNewBoardStateShadow(tempBoardstate, otherPlayerNumber, pawn, posibMoves[posiMove]);
                //  Debug.Log("What min thinks");

                m.score = MAX(Board.BoardInst.GetNewBoardStateShadow(boardState, PlayerNumber, pawn, posibMoves[posiMove]), n,currentDepth+1);
                if (m.score < best.score)
                {
                    best = m.Clone() as Move;
                }

            }
        }
        return best.score;
        // }
    }
    public float MAX(int[,,] boardState, Node node,int currentDepth)
    {
        if (maxDepth == currentDepth)
        {
            return EVAL(Board.BoardInst.ConvertToBoardState(), PlayerNumber, 9);
        }
        else
        {
            for (int pawn = 2; pawn < 5; pawn++)
            {
                List<int> posibMoves = Board.BoardInst.GetValidMoves(Board.BoardInst.ConvertToBoardState(), PlayerNumber, pawn);
                for (int posiMove = 0; posiMove < posibMoves.Count; posiMove++)
                {
                    best.score = -9999;
                    Move m = new Move(-9999, pawn, posibMoves[posiMove]);
                    //tempBoardstate = Board.BoardInst.GetNewBoardStateShadow(tempBoardstate, PlayerNumber, pawn, posibMoves[posiMove]);
                    //Debug.Log("What Max thinks");
                    //Board.BoardInst.SeeThePawnsOfShadow(Board.BoardInst.ConvertToBoardState());
                    Node n = new Node();
                    n.ParentNode = node;
                    n.move = m;
                    n.Player = PlayerNumber;
                    n.ParentNode.ChildrenNodes.Add(n);

                    m.score = MIN(Board.BoardInst.GetNewBoardStateShadow(boardState, otherPlayerNumber, pawn, posibMoves[posiMove]), n, currentDepth + 1);
                    if (m.score > best.score)
                    {
                        best = m.Clone() as Move;
                    }

                }
            }
            return best.score;
        }
    }
    public float EVAL2(int[,,] boardState, int player, int pawnMoved, int pointsForThisPlayer)
    {

        Board.BoardInst.SeeTheBoardOfShadow(boardState, 1);
        Board.BoardInst.SeeTheBoardOfShadow(boardState, 2);

        float res = Random.Range(0, 0.99f);

        res += 10 * pointsForThisPlayer;

        return res;

    }

    public float EVAL(int[,,] boardState, int player, int pawnMoved)
    {
        //Board.BoardInst.SeeTheBoardOfShadow(boardState, 1);
        //   Board.BoardInst.SeeTheBoardOfShadow(boardState, 2);
        int otherplayer;
        if (player == 1)
        {
            otherplayer = 2;
        }
        else
        {
            otherplayer = 1;
        }

        // sætter et slutresultat, som vi kan give videre. Der bliver lagt en random på til at breake ties.
        float res = 0;// + Random.Range(0.1f, 0.99f);

        return res;
    }
    public class Move : System.ICloneable
    {
        public float score;
        public int pawn;
        public int direction;

        public Move(float score, int pawn, int direction)
        {
            this.score = score;
            this.pawn = pawn;
            this.direction = direction;
        }



        public object Clone()
        {
            return new Move(this.score, this.pawn, this.direction);
        }
    }


    // Use this for initialization
    void Start()
    {
        StartCoroutine("waitforeverathing");
        if (PlayerNumber == 2)
        {
            otherPlayerNumber = 1;
        }
        else
        {
            otherPlayerNumber = 2;
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

    public void TakeTurn()
    {

        if (gm.player1sTurn == IsPlayer1)
        {

            MINIMAX(Board.BoardInst.ConvertToBoardState());
            //     Debug.Log("Best score is " + BestScore);
            //Debug.Log("I choose the best pawn to be: " + BestPawn + "and best direction is: " + BestMove+ " With best score " + BestScore);
            // System.Threading.Thread.Sleep(1000);
            MovePawn(myBest.pawn - 2, myBest.direction);


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
    // Update is called once per frame
    void Update()
    {
        /*   if (Input.GetKeyDown("e"))
           {
               Debug.Log("Player 2: " + EVAL(Board.BoardInst.ConvertToBoardState(), 2, 9));

               Debug.Log ("Player 1: " + EVAL(Board.BoardInst.ConvertToBoardState(), 1,9));
           }
           */
    }
}
