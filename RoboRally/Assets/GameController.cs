using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameController : MonoBehaviour {
    public List<PlayerBoard> PlayerBoards = new List<PlayerBoard>();
    private List<Card> _turnOrders = new List<Card>();
    public float TimeTweenMoves = 0.5f;
	// Use this for initialization
	void Start () {
    //    StartCoroutine("playCardsSlowly");
	}


    public void SaveOrders()
    {
        _turnOrders.Clear();
        for (int i = 0; i < 5; i++)
        {
            
        
        List<Card> cards = new List<Card>();


        foreach (PlayerBoard playerBoard in PlayerBoards)
        {
            cards.Add( playerBoard.Registers[i]);
        }
        
            cards = cards.OrderBy(o => o.Initiative).ToList();
            cards.Reverse();
            _turnOrders.AddRange(cards);
       }

    }

    public void TakeTurn()
    {
        StartCoroutine("ShowTurn");
    }

    IEnumerator ShowTurn()
    {
        for (int i = 0; i < _turnOrders.Count; i++)
        {
            _turnOrders[i].Player.GetComponent<GridMovement>().BroadcastMessage(_turnOrders[i].Order);
            yield return new WaitForSeconds(TimeTweenMoves);
        }

    }



    // Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp("h"))
	    {
	        SaveOrders();
            TakeTurn();
	    }
	}
}
