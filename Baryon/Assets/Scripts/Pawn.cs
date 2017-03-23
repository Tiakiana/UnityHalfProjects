using System;
using UnityEngine;
using System.Collections;

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
        Up, Right, Down, Left

    }



    void Start()
    {
        home = new Vector3(transform.position.x, transform.position.y, transform.position.z);

     

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

 

    public void Move(Directionale dir)
    {
        if (!OnBoard)
        {
            Debug.Log("Not on board");
            return;
        }
        switch (dir)
        {
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

            case Directionale.Left:

                if (CheckValidMove(dir))
                {
                    lastPos = transform.position;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = null;

                    transform.position += Vector3.left;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = gameObject;

                }


                break;

            default:
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

    public void OnMouseUp()
    {
        if (Player1Owned == GameManager.GmInst.player1sTurn)
        {
        GameManager.GmInst.ShowMovePanel(gameObject);

        }

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
