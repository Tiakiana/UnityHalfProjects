using UnityEngine;
using System.Collections;

public class GridController : MonoBehaviour
{
    //public int[,] Size = new int[20,20];
    public int XSize;
    public int YSize;
    public GameObject TileGraphic;
    public GameObject TileWallGraphic;
    public static GridController GridCtrlInstance;
    public Tile[,] Tiles;

    void Awake()
    {
        GridCtrlInstance = this;

    }
    bool withinBounds(Vector2 pos, Vector2 dir)
    {
        Vector2 posi = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
        Vector2 diri = new Vector2(Mathf.Round(dir.x), Mathf.Round(dir.y));
        Vector2 veci = posi + diri;
        bool result = false;
        if (veci.x >= 0 && veci.x < XSize && veci.y >= 0 && veci.y < YSize)
        {
            result = true;
        }
        return result;

    }

    public bool CheckForRoboCollisionMovement(Vector2 pos, Vector2 dir)
    {
        bool result = true;
        Vector2 posi = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
        Vector2 diri = new Vector2(Mathf.Round(dir.x), Mathf.Round(dir.y));
        Vector2 veci = posi + diri;
        if (withinBounds(veci, diri))
        {
            result = CheckTile(veci, diri);
            if (result)
            {
                Vector2 vecDouble = veci + diri;
                GameObject go = Tiles[(int)veci.x, (int)veci.y].Occupant;
                Tiles[(int)veci.x, (int)veci.y].Occupant.transform.position += (Vector3)diri;
                Tiles[(int)veci.x, (int)veci.y].Occupant.GetComponent<GridMovement>().SetPosition(Tiles[(int)veci.x, (int)veci.y].Occupant.transform.position);
                Tiles[(int)vecDouble.x, (int)vecDouble.y].Occupant = go;
            }

        }
        else
        {
            result = false;
        }
        return result;
    }

    public bool CheckTile(Vector2 pos, Vector2 dir)
    {
        bool passable = false;
        bool canSlide = true;
        if (withinBounds(pos, dir))
        {
            Vector2 posi = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
            Vector2 diri = new Vector2(Mathf.Round(dir.x), Mathf.Round(dir.y));
            Vector2 veci = posi + diri;
            if (Tiles[(int)veci.x, (int)veci.y].Occupant != null)
            {
                canSlide = CheckForRoboCollisionMovement(pos, dir);
            }
        }

        if (dir == new Vector2(-1, 0))
        {
            passable = Tiles[(int)pos.x, (int)pos.y].Left;
        }
        if (dir == new Vector2(1, 0))
        {
            passable = Tiles[(int)pos.x, (int)pos.y].Right;
        }
        if (dir == new Vector2(0, -1))
        {
            passable = Tiles[(int)pos.x, (int)pos.y].Down;
        }
        if (dir == new Vector2(0, 1))
        {
            passable = Tiles[(int)pos.x, (int)pos.y].Up;
        }

        bool result = passable && canSlide;
        return result;
    }

    void Start()
    {
        setGrid();
        secondPass();
    }


    private void setGrid()
    {
        Tiles = new Tile[XSize, YSize];

        for (int x = 0; x < XSize; x++)
        {
            for (int y = 0; y < YSize; y++)
            {
                GameObject obj = Instantiate(TileGraphic, new Vector2(x, y), Quaternion.identity) as GameObject;
                obj.AddComponent<Tile>().SetPosition(x, y);
                Tiles[x, y] = obj.GetComponent<Tile>();
                //Not yet implemented:
                //obj.GetComponent<Tile>().AddElement("ConveyorBeltLeft");
            }
        }

    }

    private void secondPass()
    {
        for (int x = 0; x < XSize; x++)
        {
            for (int y = 0; y < YSize; y++)
            {
                if (x == 0)
                {
                    Tiles[x, y].Left = false;
                    GameObject obj = Instantiate(TileWallGraphic, new Vector2(x, y), Quaternion.identity) as GameObject;
                    obj.transform.Rotate(Vector3.forward, 180);

                    //indsæt graphics

                }
                if (y == 0)
                {
                    Tiles[x, y].Down = false;
                    //indsæt graphics

                    GameObject obj = Instantiate(TileWallGraphic, new Vector2(x, y), Quaternion.identity) as GameObject;
                    obj.transform.Rotate(Vector3.forward, 270);

                }
                if (x == XSize - 1)
                {
                    Tiles[x, y].Right = false;
                    GameObject obj = Instantiate(TileWallGraphic, new Vector2(x, y), Quaternion.identity) as GameObject;
                }
                else
                {
                    int rnd = Random.Range(1, 11);
                    if (rnd == 10)
                    {
                        Tiles[x, y].Right = false;
                        GameObject obj = Instantiate(TileWallGraphic, new Vector2(x, y), Quaternion.identity) as GameObject;
                        Tiles[x + 1, y].Left = false;
                        GameObject obj2 = Instantiate(TileWallGraphic, new Vector2(x + 1, y), Quaternion.identity) as GameObject;
                        obj2.transform.Rotate(Vector3.forward, 180);
                    }
                    else
                    {
                        Tiles[x, y].Right = true;
                        Tiles[x + 1, y].Left = true;
                    }
                }
                if (y == YSize - 1)
                {
                    GameObject obj = Instantiate(TileWallGraphic, new Vector2(x, y), Quaternion.identity) as GameObject;
                    obj.transform.Rotate(Vector3.forward, 90);
                    Tiles[x, y].Up = false;
                }
                else
                {
                    int rnd = Random.Range(1, 11);
                    if (rnd == 10)
                    {
                        GameObject obj = Instantiate(TileWallGraphic, new Vector2(x, y), Quaternion.identity) as GameObject;
                        obj.transform.Rotate(Vector3.forward, 90);
                        GameObject obj2 = Instantiate(TileWallGraphic, new Vector2(x, y + 1), Quaternion.identity) as GameObject;
                        obj2.transform.Rotate(Vector3.forward, 270);
                        Tiles[x, y].Up = false;
                        Tiles[x, y + 1].Down = false;
                    }
                    else
                    {
                        Tiles[x, y].Up = true;
                        Tiles[x, y + 1].Down = true;
                    }
                }
            }
        }
    }
    
}
