using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HeuristicAIPlayer : MonoBehaviour
{
    public Pawn[] AIPawns = new Pawn[3];
    public bool IsPlayer1 = true;
    [SerializeField]
    private GameManager gm;
    // Use this for initialization
    void Start()
    {/*
        gm = GameManager.GmInst;

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
        */
        StartCoroutine("waitforeverathing");
    }

    private bool WillKillOpponent(Pawn pawn, Scrollbar.Direction direction)
    {

        pawn.transform.position

        bool res = false;
        return res;
    }

    public IEnumerator waitforeverathing()
    {
        yield return  new WaitForSeconds(0.5f);
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

    public void TakeTurn()
    {
        bool[] options = new bool[3];

        if (gm.player1sTurn == IsPlayer1)
        {
            foreach (Pawn pawn in AIPawns)
            {
                pawn.CheckForOptions();
            }
            RetryAgain:
            //Vælger en tilfældig pawn
            int pawnselected = Random.Range(0, 3);
            //Kigger på hvilke muligheder den pawn har og laver en liste af dem
            List<int> booleanoptions = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                if (AIPawns[pawnselected].Options[i])
                {
                    int x = i;
                    booleanoptions.Add(x);
                }
            }
            if (booleanoptions.Count == 0)
            {
                goto RetryAgain;
            }
            // rykker brikken
            int y = Random.Range(0, booleanoptions.Count);
          //  Debug.Log(AIPawns[pawnselected].name +" " + booleanoptions[y]);

            MovePawn(pawnselected,booleanoptions[y]);
          
            
        }
    }

    public void MovePawn(int pawn, int direction)
    {

        switch (direction)
        {
            case 0:
                AIPawns[pawn].Move(Pawn.Directionale.Left);
                break;
            case 1:
                AIPawns[pawn].Move(Pawn.Directionale.Up);
                break;
            case 2:
                AIPawns[pawn].Move(Pawn.Directionale.Right);
                break;
            case 3:
                AIPawns[pawn].Move(Pawn.Directionale.Down);
                break;
            case 4:
                gm.PutPawnOnBoard(AIPawns[pawn]);
                break;

            default:
                Debug.Log("No Valid move from Heuristic Player AI");
                break;
        }
        
        }
    



// Update is called once per frame
void Update()
{

}



}
