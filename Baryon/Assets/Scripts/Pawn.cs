using System;
using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour
{
    public Vector2 Coordinates;
    public bool Player1Owned;
    public Square.SquareColour Colour;
    public bool OnBoard;
    Vector3 home;

    public enum Directionale
    {
        Up, Right, Down, Left

    }

    IEnumerator SkraldetingDerskalVæk()
    {
        yield return new WaitForSeconds(0.3f);
        Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = gameObject;

    }


    void Start()
    {
        home = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //if (Player1Owned)
       // {
            
        
      //  transform.position = new Vector3(2, 0, -1);
       // StartCoroutine("SkraldetingDerskalVæk");
        //Colour = Square.SquareColour.Red;
        //}
    }

    public void Move(Directionale dir)
    {
        switch (dir)
        {
            case Directionale.Up:

                if (CheckValidMove(dir))
                {
                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = null;

                    transform.position += Vector3.up;
                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = gameObject;

                }


                break;

            case Directionale.Right:
                if (CheckValidMove(dir))
                {
                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = null;

                    transform.position += Vector3.right;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = gameObject;

                }



                break;

            case Directionale.Down:

                if (CheckValidMove(dir))
                {
                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = null;

                    transform.position += Vector3.down;

                    Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y].Occupant = gameObject;

                }



                break;

            case Directionale.Left:

                if (CheckValidMove(dir))
                {
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

    }

    public void OnMouseDown()
    {

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
                        CanIMove = true;
                    }

                }


                break;
            case Directionale.Right:
                if (transform.position.x < 4)
                {
                    if (Board.BoardInst.Squares[(int)transform.position.x + 1, (int)transform.position.y].Occupant == null)
                    {
                        CanIMove = true;
                    }

                }

                break;
            case Directionale.Down:
                if (transform.position.y > 0)
                {
                    if (Board.BoardInst.Squares[(int)transform.position.x, (int)transform.position.y - 1].Occupant == null)
                    {
                        CanIMove = true;
                    }

                }
                break;
            case Directionale.Left:
                if (transform.position.x > 0)
                {
                    if (Board.BoardInst.Squares[(int)transform.position.x - 1, (int)transform.position.y].Occupant == null)
                    {
                        CanIMove = true;
                    }

                }

                break;
            default:
                throw new ArgumentOutOfRangeException("dir", dir, null);


        }
        return CanIMove;


    }

    void Update()
    {/*
        if (Player1Owned)
        {


            if (Input.GetKeyDown("w"))
            {
                Move(Directionale.Up);
            }
            if (Input.GetKeyDown("s"))
            {
                Move(Directionale.Down);
            }
            if (Input.GetKeyDown("a"))
            {
                Move(Directionale.Left);
            }
            if (Input.GetKeyDown("d"))
            {
                Move(Directionale.Right);
            }
        }

        */
    }

    public void BeatHome() {
        MoveTo(home);
        OnBoard = false;
    }
    public void MoveTo(Vector3 vec)
    {
        transform.position = vec;

    }
}
