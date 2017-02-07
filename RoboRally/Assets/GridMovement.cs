using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    //Update Position


    public Vector2 Position;
    public Vector2 Direction;
    public string Forward, Backwards, Left, Right;

    public void SetPosition(Vector2 vec)
    {
        Position = vec;
    }

    void TurnLeft()
    {
        transform.RotateAround(transform.position, Vector3.forward, 90);
        Direction = transform.right;
    }

    void TurnRight()
    {
        transform.RotateAround(transform.position, Vector3.forward, -90);
        Direction = transform.right;
       }

    public void SetDirection(Vector2 vec)
    {
        Direction = vec;
    }

    public void DriveForward()
    {
        bool CanIDrive = GridController.GridCtrlInstance.CheckTile(transform.position, transform.right);

        if (CanIDrive)
        {
            Vector2 previousPosition = transform.position;
            Position += (Vector2)transform.right;
            Position.x = Mathf.Round(Position.x);
            Position.y = Mathf.Round(Position.y);
            transform.position = Position;
            GridController.GridCtrlInstance.Tiles[(int)Position.x, (int)Position.y].Occupant = gameObject;

            GridController.GridCtrlInstance.Tiles[(int)previousPosition.x, (int)previousPosition.y].Occupant = null;
        }
    }
    public void DriveBackwards()
    {
        bool CanIDrive = GridController.GridCtrlInstance.CheckTile(transform.position, -transform.right);
        if (CanIDrive)
        {
            Vector2 previousPosition = transform.position;

            Position -= (Vector2)transform.right;
            Position.x = Mathf.Round(Position.x);
            Position.y = Mathf.Round(Position.y);

            transform.position = Position;
            GridController.GridCtrlInstance.Tiles[(int)Position.x, (int)Position.y].Occupant = gameObject;

            GridController.GridCtrlInstance.Tiles[(int)previousPosition.x, (int)previousPosition.y].Occupant = null;


        }

    }

    
    void Start()
    {
        Position = transform.position;
    }


    void Update()
    {
        if (Input.GetKeyUp(Forward))
        {
            DriveForward();
        }
        if (Input.GetKeyUp(Backwards))
        {
            DriveBackwards();
        }
        if (Input.GetKeyUp(Left))
        {
            TurnLeft();
        }
        if (Input.GetKeyUp(Right))
        {
            TurnRight();
        }

    }
}
