using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
    public static Board BoardInst;
    public Square[,] Squares = new Square[5,5];
    public GameObject Green, Red, Blue;

    public Sprite[] SqSprites = new Sprite[5];

    void Awake()
    {
        BoardInst = this;
    }

    void Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                GameObject go = new GameObject();
                go.name = "Square " + x + "," +y;
                go.AddComponent<Square>();
                Squares[x, y] = go.GetComponent<Square>();
                go.GetComponent<Square>().Coordinates = new Vector2(x,y);
                go.GetComponent<Square>().SQColour = Square.SquareColour.Red;
                go.transform.position = new Vector3(x,y,0);
                go.transform.SetParent(transform);
                go.AddComponent<SpriteRenderer>();
                go.AddComponent<BoxCollider2D>();
                //Set colours of the squares

                if ((x == 0 && y == 0) || (x == 4 && y==0) || (x == 2 && y == 1) || (x == 0 && y == 2) || (x == 3 && y == 2) || (x == 1 && y == 3) || (x == 4 && y == 3) || (x == 3 && y == 4))
                {
                    go.GetComponent<Square>().SQColour = Square.SquareColour.Blue;
                }

                if ((x == 1 && y == 0) || (x == 0 && y == 1) || (x == 3 && y == 1) || (x == 1&& y == 2) || (x == 4 && y == 2) || (x == 2 && y == 3) || (x == 0 && y == 4) || (x == 4 && y == 4))
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
        GameObject go1 = Instantiate(Red, new Vector3(0, -1, 2), Quaternion.identity) as GameObject;
        GameObject go2 = Instantiate(Green, new Vector3(1, -1, 2), Quaternion.identity) as GameObject;
        GameObject go3 = Instantiate(Blue, new Vector3(2, -1, 2), Quaternion.identity) as GameObject;
        go1.GetComponent<Pawn>().Player1Owned = true;
        go1.name = "Player1Red";
        go1.GetComponent<Pawn>().Colour = Square.SquareColour.Red;
     

        go2.GetComponent<Pawn>().Player1Owned = true;
        go2.name = "Player1Green";
        go2.GetComponent<Pawn>().Colour = Square.SquareColour.Green;

        go3.GetComponent<Pawn>().Player1Owned = true;
        go3.name = "Player1Blue";
        go3.GetComponent<Pawn>().Colour = Square.SquareColour.Blue;


        GameObject go4 = Instantiate(Red, new Vector3(2, 5, 2), Quaternion.identity) as GameObject; 
        GameObject go5 = Instantiate(Green, new Vector3(3, 5, 2), Quaternion.identity) as GameObject;
        GameObject go6 = Instantiate(Blue, new Vector3(4, 5, 2), Quaternion.identity) as GameObject;


        go4.GetComponent<Pawn>().Player1Owned = false;
        go4.name = "Player2Red";
        go4.GetComponent<Pawn>().Colour = Square.SquareColour.Red;

        go5.GetComponent<Pawn>().Player1Owned = false;
        go5.name = "Player2Green";
        go5.GetComponent<Pawn>().Colour = Square.SquareColour.Green;

        go6.GetComponent<Pawn>().Player1Owned = false;
        go6.name = "Player2Blue";
        go6.GetComponent<Pawn>().Colour = Square.SquareColour.Blue;


        GameManager.GmInst.Player1Pawns[0] = go1;
        GameManager.GmInst.Player1Pawns[1] = go2;
        GameManager.GmInst.Player1Pawns[2] = go3;
        GameManager.GmInst.Player2Pawns[0] = go4;
        GameManager.GmInst.Player2Pawns[1] = go5;
        GameManager.GmInst.Player2Pawns[2] = go6;


    }

    public void KillColour(bool isPlayer1, Square.SquareColour colour)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (Squares[x,y].Occupant != null && Squares[x,y].SQColour == colour &&  Squares[x,y].Occupant.GetComponent<Pawn>().Player1Owned != isPlayer1)
                {
                    Squares[x,y].Occupant.GetComponent<Pawn>().BeatHome();
                    Squares[x, y].Occupant =null;

                    GameManager.GmInst.GetPointAndMove(isPlayer1);
                //    GameManager.GmInst.Moves--;
                }
            }
        }
    }

    public Board GetBoardState()
    {

        return this;
    }

    public Board GetNewBoardState()
    {
        return this;
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

}
