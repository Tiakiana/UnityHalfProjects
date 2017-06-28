using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Board : MonoBehaviour
{


    public static Board BoardInst;
    public Square[,] Squares = new Square[5, 5];
    public GameObject Green, Red, Blue;
    LastMove[,] LastMoves = new LastMove[3, 5];

    public Sprite[] SqSprites = new Sprite[5];

    void Awake()
    {
        BoardInst = this;
    }

    void Start()
    {
        
        ClearLastMoves();
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                GameObject go = new GameObject();
                go.name = "Square " + x + "," + y;
                go.AddComponent<Square>();
                Squares[x, y] = go.GetComponent<Square>();
                go.GetComponent<Square>().Coordinates = new Vector2(x, y);
                go.GetComponent<Square>().SQColour = Square.SquareColour.Red;
                go.transform.position = new Vector3(x, y, 0);
                go.transform.SetParent(transform);
                go.AddComponent<SpriteRenderer>();
                go.AddComponent<BoxCollider2D>();
                //Set colours of the squares

                if ((x == 0 && y == 0) || (x == 4 && y == 0) || (x == 2 && y == 1) || (x == 0 && y == 2) || (x == 3 && y == 2) || (x == 1 && y == 3) || (x == 4 && y == 3) || (x == 3 && y == 4))
                {
                    go.GetComponent<Square>().SQColour = Square.SquareColour.Blue;
                }

                if ((x == 1 && y == 0) || (x == 0 && y == 1) || (x == 3 && y == 1) || (x == 1 && y == 2) || (x == 4 && y == 2) || (x == 2 && y == 3) || (x == 0 && y == 4) || (x == 4 && y == 4))
                {
                    go.GetComponent<Square>().SQColour = Square.SquareColour.Green;
                }

                if ((x == 3 && y == 0) || (x == 1 && y == 1) || (x == 4 && y == 1) || (x == 2 && y == 2) || (x == 0 && y == 3) || (x == 3 && y == 3) || (x == 1 && y == 4))
                {
                    go.GetComponent<Square>().SQColour = Square.SquareColour.Red;
                }

                if ((x == 2 && y == 0))
                {
                    go.GetComponent<Square>().SQColour = Square.SquareColour.Black;
                }
                if ((x == 2 && y == 4))
                {
                    go.GetComponent<Square>().SQColour = Square.SquareColour.White;
                }


                go.GetComponent<Square>().SetGrafixColour();

                /*
                GameObject gi = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gi.transform.position = go.transform.position;
                

                gi.transform.SetParent(go.transform);

    */
            }
        }
        CreatePieces();

    }

    public void CreatePieces()
    {
        GameObject go3 = Instantiate(Blue, new Vector3(2, -1, 2), Quaternion.identity) as GameObject;
        GameObject go1 = Instantiate(Red, new Vector3(0, -1, 2), Quaternion.identity) as GameObject;
        GameObject go2 = Instantiate(Green, new Vector3(1, -1, 2), Quaternion.identity) as GameObject;

        go1.GetComponent<Pawn>().Player1Owned = true;
        go1.name = "Player1Red";
        go1.GetComponent<Pawn>().Colour = Square.SquareColour.Red;


        go2.GetComponent<Pawn>().Player1Owned = true;
        go2.name = "Player1Green";
        go2.GetComponent<Pawn>().Colour = Square.SquareColour.Green;

        go3.GetComponent<Pawn>().Player1Owned = true;
        go3.name = "Player1Blue";
        go3.GetComponent<Pawn>().Colour = Square.SquareColour.Blue;

        GameObject go6 = Instantiate(Blue, new Vector3(4, 5, 2), Quaternion.identity) as GameObject;

        GameObject go4 = Instantiate(Red, new Vector3(2, 5, 2), Quaternion.identity) as GameObject;
        GameObject go5 = Instantiate(Green, new Vector3(3, 5, 2), Quaternion.identity) as GameObject;


        go4.GetComponent<Pawn>().Player1Owned = false;
        go4.name = "Player2Red";
        go4.GetComponent<Pawn>().Colour = Square.SquareColour.Red;

        go5.GetComponent<Pawn>().Player1Owned = false;
        go5.name = "Player2Green";
        go5.GetComponent<Pawn>().Colour = Square.SquareColour.Green;

        go6.GetComponent<Pawn>().Player1Owned = false;
        go6.name = "Player2Blue";
        go6.GetComponent<Pawn>().Colour = Square.SquareColour.Blue;


        GameManager.GmInst.Player1Pawns[0] = go3;
        GameManager.GmInst.Player1Pawns[1] = go1;
        GameManager.GmInst.Player1Pawns[2] = go2;

        GameManager.GmInst.Player2Pawns[0] = go6;
        GameManager.GmInst.Player2Pawns[1] = go4;
        GameManager.GmInst.Player2Pawns[2] = go5;

    }

    public void KillColour(bool isPlayer1, Square.SquareColour colour)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (Squares[x, y].Occupant != null && Squares[x, y].SQColour == colour && Squares[x, y].Occupant.GetComponent<Pawn>().Player1Owned != isPlayer1)
                {
                    Squares[x, y].Occupant.GetComponent<Pawn>().BeatHome();
                    Squares[x, y].Occupant = null;

                    GameManager.GmInst.GetPointAndMove(isPlayer1);
                    //    GameManager.GmInst.Moves--;
                }
            }
        }
    }
    public bool IsNextToColour(Square.SquareColour colour, int x, int y)
    {
        try
        {
            if (Squares[x + 1, y].SQColour == colour)
            {
                return true;
            }
        }
        catch (System.Exception)
        {


        }

        try
        {
            if (Squares[x - 1, y].SQColour == colour)
            {
                return true;
            }
        }
        catch (System.Exception)
        {


        }
        try
        {
            if (Squares[x, y + 1].SQColour == colour)
            {
                return true;
            }
        }
        catch (System.Exception)
        {


        }
        try
        {
            if (Squares[x, y - 1].SQColour == colour)
            {
                return true;
            }
        }
        catch (System.Exception)
        {


        }
        return false;

    }

    public int HowManyWillIKill(bool isPlayer1, Square.SquareColour colour)
    {
        int res = 0;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (Squares[x, y].Occupant != null && Squares[x, y].SQColour == colour && Squares[x, y].Occupant.GetComponent<Pawn>().Player1Owned != isPlayer1)
                {
                    res++;
                    //    GameManager.GmInst.Moves--;
                }
            }
        }
        return res;
    }

    public Square[,] GetBoardState()
    {

        return Squares;
    }

    public Square[,] GetNewBoardState(Square[,] oldBoardstate, Pawn pawn, int direction)
    {
        Square[,] newBoardState = new Square[5, 5];
        return newBoardState;



    }

    public void FindPawn(int[,,] boardState, int player, int pawn, out int ex, out int yh)
    {
        ex = 9;
        yh = 9;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (boardState[x, y, 1] == player && boardState[x, y, 2] == pawn)
                {
                    ex = x;
                    yh = y;
                }
            }
        }



    }

    public void CleanUpBoard()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {

                Squares[x, y].Occupant = null;
            }
        }
    }


    // Alt Shadow play begynder her

    public int HowManyWillIKillShadow(int[,,] boardState, int player, int pawn, int direction)
    {
        int res = 0;
        // int ex = 9;
        // int yh = 9;
        // FindPawn(boardState,player,pawn,out ex,out yh);

        int ex = 0;
        int yh = 0;

        switch (direction)
        {
            case 0:
                ex = -1;
                break;

            case 1:
                yh = 1;
                break;

            case 2:
                ex = 1;
                break;

            case 3:
                yh = -1;
                break;
            default:
                break;
        }

        if (!Board.BoardInst.IsPawnOnBoard(boardState, player, pawn))
        {
            return 0;
        }
        else
        {
            for (int x = 0; x < 5; x++)
            {

                for (int y = 0; y < 5; y++)
                {
                    if (boardState[x, y, 1] != 0 && boardState[x, y, 1] != player && boardState[x, y, 0] == pawn)
                    {
                        res++;
                    }
                }
            }
            return res;

        }

    }
    public int HowManyWillIKillShadow(int[,,] boardState, int player, int pawn)
    {
        int res = 0;
        // int ex = 9;
        // int yh = 9;
        // FindPawn(boardState,player,pawn,out ex,out yh);
        if (!Board.BoardInst.IsPawnOnBoard(boardState, player, pawn))
        {
            return 0;
        }
        else
        {
            for (int x = 0; x < 5; x++)
            {

                for (int y = 0; y < 5; y++)
                {
                    if (boardState[x, y, 1] != 0 && boardState[x, y, 1] != player && boardState[x, y, 0] == pawn)
                    {
                        res++;
                    }
                }
            }
            return res;

        }

    }

    int ShadowPoints1;
    int ShadowPoints2;
    public int[,,] ConvertToBoardState()
    {
        int[,,] boardState = new int[5, 5, 3];
        ShadowPoints1 = GameManager.GmInst.Player1Points;
        ShadowPoints2 = GameManager.GmInst.Player2Points;
        //set square colour
        //0 = white, 1 = black, 2 = blue, 3 = red, 4 = green
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                boardState[x, y, 0] = (int)Squares[x, y].SQColour;
            }

        }
        //set if player and who owns it
        //0 = empty, 1 = player one owned, 2 = player two owned

        //Also set pawn colour while we're at it.
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (Squares[x, y].Occupant != null)
                {
                    if (Squares[x, y].Occupant.GetComponent<Pawn>().Player1Owned)
                    {
                        boardState[x, y, 1] = 1;
                        boardState[x, y, 2] = (int)Squares[x, y].Occupant.GetComponent<Pawn>().Colour;
                    }
                    else
                    {
                        boardState[x, y, 1] = 2;
                        boardState[x, y, 2] = (int)Squares[x, y].Occupant.GetComponent<Pawn>().Colour;

                    }
                }
                else
                {
                    boardState[x, y, 1] = 0;
                }
            }

        }
        // Set player Pawn Colour



        return boardState;
    }

    public bool IsPawnOnBoard(int[,,] boardState, int player, int pawn)
    {
        bool res = false;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (boardState[x, y, 1] == player && boardState[x, y, 2] == pawn)
                {
                    res = true;
                }
            }
        }

        return res;
    }

    public bool CheckValidShadowMove(int[,,] oldBoardState, int player, int pawn, int move)
    {
        bool res = true;


        // check om du må lægge brik på brættet
        if (move == 4)
        {
            // er brikken i forvejen på brættet?

            if (IsPawnOnBoard(oldBoardState, player, pawn))
            {

                res = false;
            }
            //Ligger der allerede en brik på startfeltet der er spillerens egen?
            if (player == 1 && oldBoardState[2, 0, 1] == 1)
            {
                res = false;
            }
            if (player == 2 && oldBoardState[2, 4, 1] == 2)
            {
                res = false;
            }
        }
        else
        {
            // er brikken på brættet?
            if (!IsPawnOnBoard(oldBoardState, player, pawn))
            {
                res = false;
            }
            else
            {
                // et sted at gemme brikkens værdier når den findes.
                int pawnPosX = 9;
                int pawnPosY = 9;
                //Find brikken
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (oldBoardState[x, y, 1] == player && oldBoardState[x, y, 2] == pawn)
                        {
                            pawnPosX = x;
                            pawnPosY = y;

                            //Debug.Log("I speculate that the " + pawn + " pawn of player " +player+ " is here: x: " + x + " y: " + y);
                            break;
                        }
                    }
                }
                switch (move)
                {
                    // kig om vi må gå til venstre
                    case 0:
                        //check om bounds
                        if (pawnPosX == 0)
                        {
                            res = false;
                        }
                        //check om empty
                        else if (oldBoardState[pawnPosX - 1, pawnPosY, 1] != 0)
                        {
                            res = false;
                        }
                        //check om vi har været der før i denne tur
                        else if (pawnPosX - 1 == LastMoves[player, pawn].XLast && pawnPosY == LastMoves[player, pawn].YLast)
                        {
                            res = false;
                        }
                        break;
                    case 1:
                        //check om bounds
                        if (pawnPosY == 4)
                        {
                            res = false;
                        }
                        //check om empty
                        else if (oldBoardState[pawnPosX, pawnPosY + 1, 1] != 0)
                        {
                            res = false;
                        }
                        //check om vi har været der før i denne tur
                        else if (pawnPosX == LastMoves[player, pawn].XLast && pawnPosY + 1 == LastMoves[player, pawn].YLast)
                        {
                            res = false;
                        }
                        break;
                    case 2:
                        //check om bounds
                        if (pawnPosX == 4)
                        {
                            res = false;
                        }
                        //check om empty
                        else if (oldBoardState[pawnPosX + 1, pawnPosY, 1] != 0)
                        {
                            res = false;
                        }
                        //check om vi har været der før i denne tur
                        else if (pawnPosX + 1 == LastMoves[player, pawn].XLast && pawnPosY == LastMoves[player, pawn].YLast)
                        {
                            res = false;
                        }
                        break;
                    case 3:
                        //check om bounds
                        if (pawnPosY == 0)
                        {
                            res = false;
                        }
                        //check om empty
                        else if (oldBoardState[pawnPosX, pawnPosY - 1, 1] != 0)
                        {
                            res = false;
                        }
                        //check om vi har været der før i denne tur
                        else if (pawnPosX == LastMoves[player, pawn].XLast && pawnPosY - 1 == LastMoves[player, pawn].YLast)
                        {
                            res = false;
                        }
                        break;

                    default:
                        Debug.Log("Trying invalid move from shadow validation");
                        break;
                }
            }
        }
        return res;
    }

    public class LastMove
    {
        public int XLast;
        public int YLast;
        public LastMove()
        {
            XLast = 9;
            YLast = 9;
        }
    }

    public bool AmIThreatening(int[,,] boardState, int player, int pawn)
    {
        bool res = false;
        if (IsPawnOnBoard(boardState, player, pawn))
        {
            int ex = 9;
            int yh = 9;

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (boardState[x, y, 1] == player && boardState[x, y, 2] == pawn)
                    {
                        ex = x;
                        yh = y;
                    }
                }
            }
            if (ex > 0)
            {
                if (boardState[ex - 1, yh, 0] == pawn)
                {
                    res = true;
                }
            }
            if (ex < 4)
            {
                if (boardState[ex + 1, yh, 0] == pawn)
                {
                    res = true;
                }
            }

            if (yh > 0)
            {
                if (boardState[ex, yh - 1, 0] == pawn)
                {
                    res = true;
                }
            }
            if (yh < 4)
            {
                if (boardState[ex, yh + 1, 0] == pawn)
                {
                    res = true;
                }
            }
        }



        return res;
    }


    public void ClearLastMoves()
    {
        for (int player = 0; player < 3; player++)
        {
            for (int pawn = 0; pawn < 5; pawn++)
            {
                LastMoves[player, pawn] = new LastMove();

            }
        }
    }

    //Makes a shadow move in a fictive world

    public int[,,] GetNewBoardStateShadow(int[,,] oldBoardState, int player, int pawn, int move)
    {

        int[,,] boardState = oldBoardState.Clone() as int[,,];
        //find pawn


        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (boardState[x, y, 1] == player && boardState[x, y, 2] == pawn)
                {
                    // LastMoves[player, pawn].XLast = x;
                    // LastMoves[player, pawn].YLast = y;
                    // Slet dens tidligere position
                    boardState[x, y, 1] = 0;
                    boardState[x, y, 2] = 0;
                    //Debug.Log("Player"+player+","+x+","+y);
                    switch (move)
                    {
                        case 0:
                            if (x > 0)
                            {
                                boardState[x - 1, y, 1] = player;
                                boardState[x - 1, y, 2] = pawn;
                                Kill(ref boardState, x - 1, y);
                            }
                            return boardState;

                        case 1:
                            if (y < 4)
                            {
                                boardState[x, y + 1, 1] = player;
                                boardState[x, y + 1, 2] = pawn;
                                Kill(ref boardState, x, y + 1);
                            }
                            return boardState;

                        case 2:
                            if (x < 4)
                            {
                                boardState[x + 1, y, 1] = player;
                                boardState[x + 1, y, 2] = pawn;
                                Kill(ref boardState, x + 1, y);
                            }
                            return boardState;

                        case 3:
                            if (y > 0)
                            {
                                boardState[x, y - 1, 1] = player;
                                boardState[x, y - 1, 2] = pawn;
                                Kill(ref boardState, x, y - 1);
                            }
                            return boardState;

                    }

                }
            }
        }
      //  Debug.Log("Either illegal move or could not find on board");

        // lægger brik på bræt;
        if (player == 1)
        {
            if (boardState[2, 0, 1] == 2)
            {
                //give point and extra move
            }
            boardState[2, 0, 1] = 1;
            boardState[2, 0, 2] = pawn;
        }
        if (player == 2)
        {
            if (boardState[2, 4, 1] == 1)
            {
                //give point and extra move
            }
            boardState[2, 4, 1] = 2;
            boardState[2, 4, 2] = pawn;
        }
        return boardState;
    }




    public int[,,] GetNewBoardStateShadow(int[,,] oldBoardState, int player, int pawn, int move, int points, out int newPoints)
    {
        newPoints = points;
        int[,,] boardState = oldBoardState.Clone() as int[,,];
        //find pawn
        for (int x = 0; x < 5; x++)
        {
            string s = "| ";
            for (int y = 4; y >= 0; y--)
            {
                s += " " + oldBoardState[x, y, 2];
            }
            //Debug.Log(s);
        }

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (boardState[x, y, 1] == player && boardState[x, y, 2] == pawn)
                {
                    // LastMoves[player, pawn].XLast = x;
                    // LastMoves[player, pawn].YLast = y;
                    // Slet dens tidligere position
                    boardState[x, y, 1] = 0;
                    boardState[x, y, 2] = 0;
                    //Debug.Log("Player"+player+","+x+","+y);
                    switch (move)
                    {
                        case 0:
                            if (x > 0)
                            {
                                boardState[x - 1, y, 1] = player;
                                boardState[x - 1, y, 2] = pawn;
                                Kill(ref boardState, x - 1, y);
                            }
                            return boardState;

                        case 1:
                            if (y < 4)
                            {
                                boardState[x, y + 1, 1] = player;
                                boardState[x, y + 1, 2] = pawn;
                                Kill(ref boardState, x, y + 1);
                            }
                            return boardState;

                        case 2:
                            if (x < 4)
                            {
                                boardState[x + 1, y, 1] = player;
                                boardState[x + 1, y, 2] = pawn;
                                Kill(ref boardState, x + 1, y);
                            }
                            return boardState;

                        case 3:
                            if (y > 0)
                            {
                                boardState[x, y - 1, 1] = player;
                                boardState[x, y - 1, 2] = pawn;
                                Kill(ref boardState, x, y - 1);
                            }
                            return boardState;

                    }

                }
            }
        }
        // lægger brik på bræt;
        if (player == 1)
        {
            if (boardState[2, 0, 1] == 2)
            {
                newPoints += 1;
            }
            boardState[2, 0, 1] = 1;
            boardState[2, 0, 2] = pawn;
        }
        if (player == 2)
        {
            if (boardState[2, 4, 1] == 1)
            {
                newPoints += 1;
            }
            boardState[2, 4, 1] = 2;
            boardState[2, 4, 2] = pawn;
        }

        return boardState;
    }

    public void SeeTheBoardOfShadow(int[,,] boardState, int i)
    {
        switch (i)
        {
            case 0:
                Debug.Log("Showing squares");
                break;

            case 1:
                Debug.Log("showing player ownage");
                break;

            case 2:
                Debug.Log("showing player pawn");
                break;


            default:
                break;
        }
        string s = "";

        for (int y = 4; y >= 0; y--)
        {
            for (int x = 0; x < 5; x++)
            {
                s += boardState[x, y, i] + "  ";
            }
            s += "\n";// Debug.Log(s);
        }
        Debug.Log(s);
    }

    public void SeeThePawnsOfShadow(int[,,] boardState)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (boardState[x, y, 2] == 2)
                {

                }
            }
        }
    }

    public void Kill(ref int[,,] currentBoard, int x, int y)
    {
        if (currentBoard[x, y, 0] == currentBoard[x, y, 2])
        {
            int player = currentBoard[x, y, 1];
            int colour = currentBoard[x, y, 0];
            for (int ex = 0; ex < 5; ex++)
            {
                for (int yh = 0; yh < 5; yh++)
                {
                    if (currentBoard[ex, yh, 0] == colour && currentBoard[ex, yh, 1] != 0 && currentBoard[ex, yh, 1] != player)
                    {
                        currentBoard[ex, yh, 1] = 0;
                        currentBoard[ex, yh, 2] = 0;
                        //Giv point og ekstra tur
                    }
                }
            }
        }

    }
    // get Valid Moves for pawn
    public List<int> GetValidMoves(int[,,] boardState, int player, int pawn)
    {
        List<int> validMoves = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            if (CheckValidShadowMove(boardState, player, pawn, i))
            {
                validMoves.Add(i);
            }
        }
        return validMoves;

    }

    // Time for testing
    void Update()
    {
/*

        if (Input.GetKeyUp("g"))
        {

            Debug.Log("Doing stuff");
            string s = "";
            foreach (int item in GetValidMoves(ConvertToBoardState(), 2, 2))
            {
                s += " " + item;
            }
            Debug.Log(s);


            //  Debug.Log(ConvertToBoardState()[0,1]);
        }
        if (Input.GetKeyDown("q"))
        {

            //     SeeTheBoardOfShadow(ConvertToBoardState(), 0);
            //     SeeTheBoardOfShadow(ConvertToBoardState(), 1);
            // Debug.Log("Nuværende brætstadie");
            //SeeTheBoardOfShadow(ConvertToBoardState(), 2);
            //Debug.Log("Hvis jeg rykker blå for spiller 1 en frem");
            // SeeTheBoardOfShadow(GetNewBoardStateShadow(ConvertToBoardState(),1,2,0),2);
            // SeeTheBoardOfShadow(GetNewBoardStateShadow(ConvertToBoardState(), 1, 2, 1), 2);
            // SeeTheBoardOfShadow(GetNewBoardStateShadow(ConvertToBoardState(),2,4,5),2);
            List<int> validmoves = GetValidMoves(ConvertToBoardState(), 2, 2);
            for (int x = 2; x < 5; x++)
            {
                validmoves = GetValidMoves(ConvertToBoardState(), 2, x);
                string s = "";
                for (int i = 0; i < validmoves.Count; i++)
                {
                    s += " " + validmoves[i];
                }

                Debug.Log(s);
            }
        }
    */
        if (Input.GetKeyUp("j"))
        {
       int [,] res = ConvertBoardToANNInput();
            for (int y = 4; y >= 0; y--)
            {
                string ris = "";
                for (int x = 0; x < 5; x++)
                {
                    ris += " " + res[x, y];
                }
                Debug.Log(ris);
            }
            Debug.Log("P1: " + res[5,0]);
            Debug.Log("P2: " + res[6, 0]);




        }



    }

    public int[,] ConvertBoardToANNInput()
    {
        int[,,] boardstate = ConvertToBoardState();
        int[,] result = new int[5,5];

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                result[x, y] = 1;
                if (boardstate[x,y,1] == 1 && boardstate[x,y,2] == 2)
                {
                    //spiller 1 blå
                    result[x, y] = 2;
                }
                if (boardstate[x, y, 1] == 1 && boardstate[x, y, 2] == 3)
                {
                    //spiller 1 rød
                    result[x, y] = 3;
                }
                if (boardstate[x, y, 1] == 1 && boardstate[x, y, 2] == 4)
                {
                    //spiller 1 Grøn
                    result[x, y] = 4;
                }
                if (boardstate[x, y, 1] == 2 && boardstate[x, y, 2] == 2)
                {
                    //spiller 2 blå
                    result[x, y] = 5;
                }
                if (boardstate[x, y, 1] == 2 && boardstate[x, y, 2] == 3)
                {
                    //spiller 2 rød
                    result[x, y] = 6;
                }
                if (boardstate[x, y, 1] == 2 && boardstate[x, y, 2] == 4)
                {
                    //spiller 2 Grøn
                    result[x, y] = 7;
                }

            }
        }
     //   result[5, 0] = GameManager.GmInst.Player1Points;
      //  result[6, 0] = GameManager.GmInst.Player2Points;
        return result;
       

    }


}
