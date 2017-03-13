using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
    public static Board BoardInst;
    public Square[,] Squares = new Square[5,5];

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


    }

    public void KillColour(bool isPlayer1, Square.SquareColour colour)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (Squares[x,y].Occupant != null && Squares[x,y].SQColour == colour &&  Squares[x,y].Occupant.GetComponent<Pawn>().Player1Owned != isPlayer1)
                {
                    Destroy(Squares[x,y].Occupant);
                    // Give points to player
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


}
