using System;
using UnityEngine;
using System.Collections;
using System.Threading;

public class Pawn : MonoBehaviour
{
    public Vector2 Coordinates;
    public bool Player1Owned;
    public Square.SquareColour Colour;
    public bool OnBoard;
    Vector3 home, lastPos;
    public bool OptionMoveLeft, OptionMoveUp, OptionMoveRight, OptionMoveDown, OptionMoveIn;
    //Left, Up, right, down, in
    public bool[] Options = new bool[5];

    public enum Directionale
    {
        Up = 0, Right = 1, Down = 2, Left =3, In = 4

    }



    void Start()
    {
        home = new Vector3(transform.position.x, transform.position.y, transform.position.z);

     

    }

    public static Directionale ConvertIntToDir(int dir) {
        Directionale res = Directionale.In;
        switch (dir)
        {
            
            case 0:
                res = Directionale.Left;
                break;
            case 1:
                res = Directionale.Up;
                break;
            case 2:
                res = Directionale.Right;
                break;
            case 3:
                res = Directionale.Down;
                break;
            case 4:
                res = Directionale.In;
                break;
            default:
                res = Directionale.In;
                break;
        }
        return res;
    }

    public static Square GetSquareInDirection(Pawn pawn, Directionale dir) {
        Square res = null;
        if (CheckValidMove(pawn,dir))
        {
            switch (dir)
            {
                case Directionale.Up:
                    res = Board.BoardInst.Squares[(int)pawn.transform.position.x, (int)pawn.transform.position.y+1];
                    break;
                case Directionale.Right:
                    res = Board.BoardInst.Squares[(int)pawn.transform.position.x+1, (int)pawn.transform.position.y];
                    break;
                case Directionale.Down:
                    res = Board.BoardInst.Squares[(int)pawn.transform.position.x, (int)pawn.transform.position.y - 1];

                    break;
                case Directionale.Left:
                    res = Board.BoardInst.Squares[(int)pawn.transform.position.x-1, (int)pawn.transform.position.y];
                    break;

                case Directionale.In:
                    if (pawn.Player1Owned)
                    {
                        res = Board.BoardInst.Squares[2, 0];
                    }
                    else
                    {
                        res = Board.BoardInst.Squares[2, 4];
                    }
                    break;
                default:
                    Debug.Log("Pawn could not find a square");
                    break;
            }
        }
        return res;
    }

    public void CheckForOptions()
    {
        OptionMoveUp = false;
        OptionMoveDown = false;
        OptionMoveIn = false;
        OptionMoveLeft = false;
        OptionMoveRight = false;
        for (int i = 0; i < 5; i++)
        {
            Options[i] = false;
        }
        if (OnBoard)
        {
            if (CheckValidMove(Directionale.Up))
            {
                OptionMoveUp = true;
                Options[1] = true;

            }
            if (CheckValidMove(Directionale.Down))
            {
                OptionMoveDown = true;
                Options[3] = true;

            }
            if (CheckValidMove(Directionale.Right))
            {
                OptionMoveRight = true;
                Options[2] = true;

            }
            if (CheckValidMove(Directionale.Left))
            {
                OptionMoveLeft = true;
                Options[0] = true;

            }
        }
        else
        {
            if (Player1Owned)
            {
                if (Board.BoardInst.Squares[2, 0].Occupant == null || Board.BoardInst.Squares[2,0].Occupant.GetComponent<Pawn>().Player1Owned == false)
                {
                    OptionMoveIn = true;
                    Options[4] = true;
                }


            }
            else
            {
                if (Board.BoardInst.Squares[2, 4].Occupant == null || Board.BoardInst.Squares[2, 4].Occupant.GetComponent<Pawn>().Player1Owned)
                {
                    OptionMoveIn = true;
                    Options[4] = true;
                }

            }
        }
    }

    public bool[] CheckForOptions(Square[,] boardState)
    {
        OptionMoveUp = false;
        OptionMoveDown = false;
        OptionMoveIn = false;
        OptionMoveLeft = false;
        OptionMoveRight = false;
        bool[] options = new bool[5];

        for (int i = 0; i < 5; i++)
        {
            options[i] = false;
        }
        if (OnBoard)
        {
            if (CheckValidMove(Directionale.Up))
            {
                OptionMoveUp = true;
                options[1] = true;

            }
            if (CheckValidMove(Directionale.Down))
            {
                OptionMoveDown = true;
                options[3] = true;

            }
            if (CheckValidMove(Directionale.Right))
            {
                OptionMoveRight = true;
                options[2] = true;

            }
            if (CheckValidMove(Directionale.Left))
            {
                OptionMoveLeft = true;
                options[0] = true;

            }
        }
        else
        {
            if (Player1Owned)
            {
                if (Board.BoardInst.Squares[2, 0].Occupant == null || Board.BoardInst.Squares[2, 0].Occupant.GetComponent<Pawn>().Player1Owned == false)
                {
                    OptionMoveIn = true;
                    options[4] = true;
                }


            }
            else
            {
                if (Board.BoardInst.Squares[2, 4].Occupant == null || Board.BoardInst.Squares[2, 4].Occupant.GetComponent<Pawn>().Player1Owned)
                {
                    OptionMoveIn = true;
                    options[4] = true;
                }

            }
        }
        return options;
    }
    

    public void Move(Directionale dir)
    {
        Thread.Sleep(1);
        if (!OnBoard)
        {
            PutOnBoard(this);
            return;
        }
        switch (dir)
        {
            case Directionale.Left:

                if (CheckValidMove(dir))
                {
                    lastPos = transform.position;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = null;

                    transform.position += Vector3.left;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = gameObject;

                }

                break;

            case Directionale.Up:

                if (CheckValidMove(dir))
                {
                    lastPos = transform.position;
                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = null;

                    transform.position += Vector3.up;
                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = gameObject;
                }
                
                break;

            case Directionale.Right:
                if (CheckValidMove(dir))
                {
                    lastPos = transform.position;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = null;

                    transform.position += Vector3.right;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = gameObject;

                }
                
                break;

            case Directionale.Down:

                if (CheckValidMove(dir))
                {
                    lastPos = transform.position;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = null;

                    transform.position += Vector3.down;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = gameObject;

                }
                
                break;

          

            default:
                Debug.Log(dir);
                throw new ArgumentOutOfRangeException("dir", dir, null);
        }
        if (Board.BoardInst.Squares[(int)transform.position.x,(int)transform.position.y].SQColour == Colour)
        {
            Board.BoardInst.KillColour(Player1Owned, Colour);

        }
        if (GameManager.GmInst.GameMasterMode)
        {
            ResetLastPosition();
        }
        GameManager.GmInst.Moves--;

    }
    public void PutOnBoard(Pawn pawn)
    {
        GameManager.GmInst.PutPawnOnBoard(pawn);
    }
    public void OnMouseUp()
    {
        if (Player1Owned == GameManager.GmInst.player1sTurn)
        {
        GameManager.GmInst.ShowMovePanel(gameObject);

        }

    }

    

    public bool CheckValidMove(Square[,] boardState, Pawn pawn, Directionale dir)
    {
        bool CanIMove = false;
        switch (dir)
        {
            case Directionale.Up:

                if (transform.position.y < 4)
                {
                    if (boardState[(int)pawn.transform.position.x, (int)pawn.transform.position.y + 1].Occupant == null)
                    {
                        Vector3 vec = pawn.transform.position + Vector3.up;
                        if (vec!= lastPos)
                        {
                        CanIMove = true;
                        }
                    }
                }
                break;

            case Directionale.Right:
                if (transform.position.x < 4)
                {
                    if (boardState[(int)transform.position.x + 1, (int)transform.position.y].Occupant == null)
                    {
                        Vector3 vec = transform.position + Vector3.right;
                        if (vec != lastPos)
                        {
                            CanIMove = true;
                        }
                    }
                }
                break;

            case Directionale.Down:
                if (transform.position.y > 0)
                {
                    if (Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y - 1].Occupant == null)
                    {
                        Vector3 vec = transform.position + Vector3.down;
                        if (vec != lastPos)
                        {
                            CanIMove = true;

                        }
                    }

                }
                break;
            case Directionale.Left:
                if (transform.position.x > 0)
                {
                    if (Board.BoardInst.Squares[(int)transform.position.x - 1, (int)transform.position.y].Occupant == null)
                    {
                        Vector3 vec = transform.position + Vector3.left;
                        if (vec != lastPos)
                        {
                            CanIMove = true;

                        }
                    }

                }

                break;
            default:
                throw new ArgumentOutOfRangeException("dir", dir, null);


        }
        return CanIMove;


    }
    public bool CheckValidMove(Directionale dir)
    {
        bool CanIMove = false;
        switch (dir)
        {
            case Directionale.Up:

                if (transform.position.y < 4)
                {
                    if (Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y + 1].Occupant == null)
                    {
                        Vector3 vec = transform.position + Vector3.up;
                        if (vec!= lastPos)
                        {
                        CanIMove = true;

                        }
                    }

                }


                break;
            case Directionale.Right:
                if (transform.position.x < 4)
                {
                    if (Board.BoardInst.Squares[(int)transform.position.x + 1, (int)transform.position.y].Occupant == null)
                    {
                        Vector3 vec = transform.position + Vector3.right;
                        if (vec != lastPos)
                        {
                            CanIMove = true;

                        }
                    }

                }

                break;
            case Directionale.Down:
                if (transform.position.y > 0)
                {
                    if (Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y - 1].Occupant == null)
                    {
                        Vector3 vec = transform.position + Vector3.down;
                        if (vec != lastPos)
                        {
                            CanIMove = true;

                        }
                    }

                }
                break;
            case Directionale.Left:
                if (transform.position.x > 0)
                {
                    if (Board.BoardInst.Squares[(int)transform.position.x - 1, (int)transform.position.y].Occupant == null)
                    {
                        Vector3 vec = transform.position + Vector3.left;
                        if (vec != lastPos)
                        {
                            CanIMove = true;

                        }
                    }

                }

                break;
            default:
                throw new ArgumentOutOfRangeException("dir", dir, null);


        }
        return CanIMove;


    }

    public static bool CheckValidMove(Pawn pawn, Directionale dir)
    {
        bool CanIMove = false;
        switch (dir)
        {
            case Directionale.Up:

                if (pawn.transform.position.y < 4)
                {
                    if (Board.BoardInst.Squares[(int)pawn.transform.position.x, (int)pawn.transform.position.y + 1].Occupant == null)
                    {
                        Vector3 vec = pawn.transform.position + Vector3.up;
                        if (vec != pawn.lastPos)
                        {
                            CanIMove = true;

                        }
                    }

                }


                break;
            case Directionale.Right:
                if (pawn.transform.position.x < 4)
                {
                    if (Board.BoardInst.Squares[(int)pawn.transform.position.x + 1, (int)pawn.transform.position.y].Occupant == null)
                    {
                        Vector3 vec = pawn.transform.position + Vector3.right;
                        if (vec != pawn.lastPos)
                        {
                            CanIMove = true;

                        }
                    }

                }

                break;
            case Directionale.Down:
                if (pawn.transform.position.y > 0)
                {
                    if (Board.BoardInst.Squares[(int)pawn.transform.position.x, (int)pawn.transform.position.y - 1].Occupant == null)
                    {
                        Vector3 vec = pawn.transform.position + Vector3.down;
                        if (vec != pawn.lastPos)
                        {
                            CanIMove = true;

                        }
                    }

                }
                break;
            case Directionale.Left:
                if (pawn.transform.position.x > 0)
                {
                    if (Board.BoardInst.Squares[(int)pawn.transform.position.x - 1, (int)pawn.transform.position.y].Occupant == null)
                    {
                        Vector3 vec = pawn.transform.position + Vector3.left;
                        if (vec != pawn.lastPos)
                        {
                            CanIMove = true;

                        }
                    }

                }
                

                break;
            case Directionale.In:
                if (pawn.Player1Owned)
                {
                    if (Board.BoardInst.Squares[2, 0].Occupant == null)
                    {
                        CanIMove = true;
                    }
                    else
                    {
                        if (!Board.BoardInst.Squares[2,0].Occupant.GetComponent<Pawn>().Player1Owned)
                        {
                            CanIMove = true;
                        }
                    }

                }
                else
                {
                    if (Board.BoardInst.Squares[2, 4].Occupant == null)
                    {
                        CanIMove = true;
                    }
                    else
                    {
                        if (Board.BoardInst.Squares[2, 4].Occupant.GetComponent<Pawn>().Player1Owned)
                        {
                            CanIMove = true;
                        }
                    }
                }
                break;

            default:
                throw new ArgumentOutOfRangeException("dir", dir, null);


        }
        return CanIMove;


    }


    public void ResetLastPosition()
    {
        lastPos = new Vector3(9,9,9);
    }

    void Update()
    {
       
    }

    public void BeatHome() {
        MoveTo(home);
        ResetLastPosition();
        OnBoard = false;
    }
    public void MoveTo(Vector3 vec)
    {
        transform.position = vec;

    }
}
