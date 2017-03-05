using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
    public GameObject PanelOfButtons,InventoryPanel;
    public GameObject ButtonPrefab;
    public GameObject ColumnPrefab,ButtonInventory;
    public Sprite[] Sprites = new Sprite[6];
    public Texture2D[] Textures = new Texture2D[6];
    public static GameManager GmInst;
    public ButtonScr Held;
    public ButtonScr Clicked;
    List<ButtonScr> fakeInventory;
    public GameObject Coin;
    public Transform SpawnPoint;
    public float Timers = 100;
    public Text TimerText;
    //Hende den kønnes kode
    public ButtonScr[,] Board = new ButtonScr[9,9];
    public int Level = 1;
    public int Moves = 1;
    public int Exp;
    public ButtonScr[,] testBoard = new ButtonScr[9, 9];
    public int Size;
    ButtonScr[] Inventory = new ButtonScr[9];

    void Awake() {
        GmInst = this;
    }

    void Update() {
       // Debug.Log(Moves);
        if (Input.GetKeyDown("h"))
        {
            React();

        }

        Timers -= Time.deltaTime;
        TimerText.text = Timers.ToString();
        if (Timers <=0)
        {
            SceneManager.LoadScene(1);
        }

    }

    void StartGame() {
        Level = 1;
        Moves = 1;
        Debug.Log(Level);
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                
                Board[i, j].Item = Random.Range(1, 7);
                Board[i, j].GetComponent<Image>().sprite = Sprites[Board[i,j].Item-1];
            }
        }
    //    Moves = Level;
       
    }

    void Start () {
        Moves = 1;
        Level = 1;
        for (int i = 0; i < 9; i++)
        {
      GameObject go = Instantiate(ColumnPrefab,PanelOfButtons.transform) as GameObject;
            go.name = "Culumn" + i;
            for (int j = 0; j < 9; j++)
            {
                GameObject go1 = Instantiate(ButtonPrefab,go.transform) as GameObject;
                go1.GetComponent<ButtonScr>().Coordinates = new Vector2(i,j);
                Board[i, j] = go1.GetComponent<ButtonScr>();
            }
        }
        StartGame();
        Moves = 1;
        Level = 1;
        Debug.Log(Moves);
        Exp = 0;
        //Create inventory
        for (int i = 0; i < 9; i++)
        {
            GameObject go2 = Instantiate(ButtonInventory,InventoryPanel.transform) as GameObject;
            go2.GetComponent<ButtonScr>().Item = Random.Range(1, 7);
             go2.GetComponent<Image>().sprite = Sprites[go2.GetComponent<ButtonScr>().Item-1];
            go2.GetComponent<ButtonScr>().Inventory = true;
            go2.GetComponent<ButtonScr>().Coordinates = new Vector2(i,0);
        }
        
        React();

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject go4 = new GameObject();
                go4.AddComponent<ButtonScr>();
                testBoard[i, j] = go4.GetComponent<ButtonScr>();
                testBoard[i, j].Item = Board[i, j].Item;
            }
        }

        //fill the fake inventory
        fakeInventory = new List<ButtonScr>(Inventory);

        //fill the fake board
        
      
    }
    //An explosion of motion
    //Counts combo, exp, deletes and spawns new items
    public void React() {
        int combo = 0;
        while (!Check()) {
            Vector2 rum = CheckV();
            int type = Board[(int)rum.x,(int)rum.y].Item;
            Size = 0;
            Size += DelEast((int)rum.x, (int)rum.y, Board[(int)rum.x, (int)rum.y].Item);
            Fill();
            combo++;
            UpdateBoard();
        }
        Exp = Exp + combo;
    }
    //Fills new gems into empty spaces
    public void Fill() {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (Board[i,j].Item == 0)
                {
                    Board[i, j].Item = Random.Range(1,7);
                }
            }
        }

    }
    
    public int DelNorth(int x, int y, int item) {
        if (Board[x,y].Item == item)
        {
            Board[x, y].Item = 0;
            if (x>0)
            {

                Size += DelWest(x - 1, y, item);
            }
            if (x<8)
            {
                Size += DelEast(x+1,y,item);
            }
            if (y>0)
            {
                Size += DelNorth(x, y - 1, item);
            }
            return 1;
        }
        
            return 0;
    }

    public int DelEast(int x, int y, int item)
    {
        if (Board[x, y].Item == item)
        {
            Board[x, y].Item = 0;
            if (x < 8)
            {

                Size += DelEast(x + 1, y, item);
            }
            if (y > 0)
            {
                Size += DelNorth(x, y-1, item);
            }
            if (y < 8)
            {
                Size += DelSouth(x, y + 1, item);
            }
            return 1;
        }
        return 0;
    }

    public int DelWest(int x, int y, int item)
    {
        if (Board[x, y].Item == item)
        {
            Board[x, y].Item = 0;
            if (x > 0)
            {

                Size += DelWest(x - 1, y, item);
            }
            if (y > 0)
            {
                Size += DelNorth(x, y-1, item);
            }
            if (y < 8)
            {
                Size += DelSouth(x, y + 1, item);
            }
            return 1;
        }
        return 0;
    }

    public int DelSouth(int x, int y, int item)
    {
        if (Board[x, y].Item == item)
        {
            Board[x, y].Item = 0;
            if (x > 0)
            {

                Size += DelWest(x - 1, y, item);
            }
            if (x < 8)
            {
                Size += DelEast(x + 1, y, item);
            }
            if (y < 8)
            {
                Size += DelSouth(x, y + 1, item);
            }
            
            return 1;
        }
        return 0;
    }

    //Checks if there is a 3 on a line match
    public bool Check() {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (i<7)
                {
                    if (Board[i+1,j].Item == Board[i,j].Item && Board[i+2,j].Item == Board[i,j].Item)
                    {
                        return false;
                    }
                }
                if (j < 7)
                {
                    if (Board[i, j].Item == Board[i, j+1].Item && Board[i, j+2].Item == Board[i, j].Item)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public bool CheckTestBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (i < 7)
                {
                    if (testBoard[i + 1, j].Item == testBoard[i, j].Item && testBoard[i + 2, j].Item == testBoard[i, j].Item)
                    {
                        return false;
                    }
                }
                if (j < 7)
                {
                    if (testBoard[i, j].Item == testBoard[i, j + 1].Item && testBoard[i, j + 2].Item == testBoard[i, j].Item)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void UpdateBoard() {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Board[i, j].GetComponent<Image>().sprite = Sprites[Board[i, j].Item - 1];
            }
        }

    }

    public Vector2 CheckV()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (i < 7)
                {
                    if (Board[i + 1, j].Item == Board[i, j].Item && Board[i+2,j].Item == Board[i, j].Item)
                    {
                        return new Vector2(i,j);
                    }
                }
                if (j < 7)
                {
                    if (Board[i, j].Item == Board[i, j + 1].Item && Board[i, j + 2].Item == Board[i, j].Item)
                    {
                        return new Vector2(i, j);
                    }
                }
            }
        }
        Debug.Log("FAAAAAAACK");
        return new Vector2(100,100);
    }



 
    public void EvilMove(ButtonScr btn) {
        if (Held != null)
        {
            testBoard[(int)btn.Coordinates.x, (int)btn.Coordinates.y].Item = Held.Item;
            
        }
        Moves--;
        if (Moves == 0)
        {
            if (!CheckTestBoard())
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Board[i, j].Item = testBoard[i, j].Item;
                     
                    }
                }
                React();
                SpawnMonster();
                UpdateBoard();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        testBoard[i, j].Item = Board[i, j].Item;
                    }
                }
                Moves = Level;
                Held = null;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            }
            else
            {
                Debug.Log("Would've rolled back and dabbed!");
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        testBoard[i, j].Item = Board[i, j].Item;
                    }
                }
                Held = null;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                Moves = Level;
            }
        }


       // Debug.Log("Select where to place gem");
        UpdateBoard();
    }


   // public void EvilTurn() {
        
  //      testBoard = Board;
      
    //    int moves = Level;
  //  }


    public void SpawnMonster() {
       GameObject go =  Instantiate(Coin, SpawnPoint.position, Quaternion.identity) as GameObject;
        go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-4, 5), Random.Range(-4, 5)));
    }
	
	// Update is called once per frame

}
