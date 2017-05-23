using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

public class QLearnP2 : MonoBehaviour
{
    Dictionary<int, QNode> ListOfStates = new Dictionary<int, QNode>();
    List<Action> usedActions = new List<Action>();
    //Unity Specifikt
    public float ExploratoryMoveChance = 10;
    public Pawn[] AIPawns = new Pawn[3];
    public int PlayerNumber;
    public int otherPlayerNumber;
    public GameManager gm;
    // save after x number of games;
    public int saveInterval = 100;
    int GameCount = 0;
   // public bool Move;
    public bool IsPlayer1;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("waitforeverathing");
        if (PlayerNumber == 2)
        {
            otherPlayerNumber = 1;
        }
        else
        {
            otherPlayerNumber = 2;
        }
        // Load dict if file exists.
        var path = "QlearnDict2.xml";
        if (File.Exists(path))
        {
            ListOfStates = GetListOfStates();
            //Debug.Log("I load list");
        }

    }

    public IEnumerator waitforeverathing()
    {
        yield return new WaitForSeconds(0.5f);
        //    gm = GameManager.GmInst;

        if (IsPlayer1)
        {
            for (int i = 0; i < 3; i++)
            {
                AIPawns[i] = gm.Player1Pawns[i].GetComponent<Pawn>();
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                AIPawns[i] = gm.Player2Pawns[i].GetComponent<Pawn>();
            }
        }
    }


    public class QNode
    {
        public int BoardState;
        public List<Action> Actions;

        public QNode( int boardState)
        {
            BoardState = boardState;
            Actions = new List<Action>();
        }
        public QNode() { }
      

    }
 


    public class Action
    {
        public int Pawn;
        public int Move;
        public float Score;

        public Action(int pawn, int move)
        {
            Pawn = pawn;
            Move = move;
            Score = 0.5f;
        }
        public Action() {

        }
    }
    /*
     * public void Traverse(NTree<T> node, TreeVisitor<T> visitor)
    {
        visitor(node.data);
        foreach (NTree<T> kid in node.children)
            Traverse(kid, visitor);
    }
     * */

    //public List<int> NumberOfActionsPrGame = new List<int>();
    public void GameOver(bool didIWin) {
        
        if (didIWin)
        {
            //Chokolade til mig
            foreach (Action item in usedActions)
            {
                item.Score += (1f/usedActions.Count);
            }
        }
        else
        {
            //skridtprygl;
            foreach (Action item in usedActions)
            {
                item.Score -= .001f;
            }

        }
   //     NumberOfActionsPrGame.Add(usedActions.Count);
        usedActions.Clear();
        if (GameCount == saveInterval)
        {
            SaveListOfStates(ListOfStates);
            GameCount = 0;
            Debug.Log("I Save");
        }
        GameCount++;
    }
    public int UnknownStates = 0;
    public void TakeTurn()
    {
        int Boardstate = HashBoardState();
        if (ListOfStates.ContainsKey(Boardstate))
        {
            // Kig dig om for at se hvad du så skal gøre
          //  Debug.Log("I Know this state!");

     //       Debug.Log("Best action here is:");

            List<Action> SortedList = ListOfStates[Boardstate].Actions.OrderBy(o => o.Score).ToList();
            SortedList.Reverse();
         //   Debug.Log("Pawn:" + SortedList[0].Pawn + " Move: " + SortedList[0].Move + " With a score of: " + SortedList[0].Score);
            if (Random.Range(1,101)< ExploratoryMoveChance)
            {
                if (SortedList.Count>1)
                {
                    MakeMove(SortedList[Random.Range(1, SortedList.Count)]);
                }
                else
                {
                    MakeMove(SortedList[0]);
                }
            }
            else
            {
         
                    MakeMove(SortedList[0]);
            }
        }

        // Hvis IKKE vi kender dette boardstate
        else
        {
            //   Debug.Log("I Dont know this state :(");
            UnknownStates++;
            if (UnknownStates>10000)
            {
                Debug.Log("Resetting unknownStates");
                UnknownStates = 0;
            }
            QNode qn = new QNode(Boardstate);
            for (int pawn = 0; pawn < 3; pawn++)
            {
                AIPawns[pawn].CheckForOptions();
                for (int move = 0; move < 5; move++)
                {
                    if (AIPawns[pawn].Options[move])
                    {
                        qn.Actions.Add(new Action(pawn,move));
                    }
                }
            }
            ListOfStates.Add(Boardstate, qn);
            MakeMove(qn.Actions[Random.Range(0,qn.Actions.Count)]);
        }


    }




    // Boardstate har jeg
    // Har jeg set det før?
    // hvis ja: Hvad gjorde jeg sidste gang der havde høj score
    //hvis nej: Add det her boardstate til vidensbase og gør noget random
    // On Game End: Vandt jeg? Så skal jeg have kage og bonusser
    //On Game End: Vandt jeg ikke? Så skal jeg have skridtprygl og straf

    public void MakeMove(Action action) {
        usedActions.Add(action);
        AIPawns[action.Pawn].Move(Pawn.ConvertIntToDir(action.Move));
      //  Debug.Log("USed Actions So far: " + usedActions.Count); 
    }

    public int HashBoardState()
    {
        int[,,] ex = Board.BoardInst.ConvertToBoardState();
        string s = "";
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    s += ex[x, y, z];

                }
            }
        }
        int hashedString = s.GetHashCode();
        return hashedString;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("s"))
        {
            Debug.Log("Saving");
            SaveListOfStates(ListOfStates);
        }

        if (Input.GetKeyUp("n"))
        {
           

        }


    }
    //Save QNode XML fil
    public void SaveListOfStates(Dictionary<int, QNode> listOfStates)
    {
        List<QNode> qList = new List<QNode>();
        foreach (KeyValuePair<int, QNode> item in listOfStates)
        {
            qList.Add(item.Value);
        }
        XmlSerializer writer = new XmlSerializer(typeof(List<QNode>));

        var path = "QlearnDict2" + ".xml";

        FileStream file = File.Create(path);


        writer.Serialize(file, qList);

        file.Close();
    }
    public Dictionary<int, QNode> GetListOfStates()
    {
        XmlSerializer reader = new XmlSerializer(typeof(List<QNode>));

        var path = "QlearnDict2.xml";

        StreamReader file = new StreamReader(path);
        List<QNode> qList = (List<QNode>)reader.Deserialize(file);
        Dictionary<int, QNode> listOfStates = new Dictionary<int, QNode>();
        foreach (QNode item in qList)
        {
            listOfStates.Add(item.BoardState, item);
        }

        file.Close();

        return listOfStates;
    }
}
