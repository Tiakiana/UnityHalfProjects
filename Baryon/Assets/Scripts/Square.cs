using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

    public Vector2 Coordinates;
    public enum SquareColour {
        White = 0,
        Black,
        Blue,
        Red,
        Green

    }

    public SquareColour SQColour;
    public GameObject Occupant;

    public void SetGrafixColour()
    {
        GetComponent<SpriteRenderer>().sprite = Board.BoardInst.SqSprites[(int)SQColour];
    }

    void OnMouseDown()
    {
      //  GameManager.GmInst.SquarePointedTo = gameObject;
    }

    void OnMouseUp()
    {
     //   GameManager.GmInst.SquarePointedTo = null;

    }
}
