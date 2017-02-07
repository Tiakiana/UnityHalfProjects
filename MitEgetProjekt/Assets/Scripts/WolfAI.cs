using UnityEngine;
using System.Collections;
using Behave.Runtime;

public class WolfAI : MonoBehaviour,IAgent {

    Behave.Runtime.Tree m_Tree;

    //Dette er grundstenen i Behave. I stedet for BLNewBehaveLibrary, skriver man sit eget navn

    WolfStats WS;
    NavMeshAgent nma;


    IEnumerator Start()
    {
        WS = GetComponent<WolfStats>();
        nma = GetComponent<NavMeshAgent>();
        m_Tree = BLWolf.InstantiateTree(
            BLWolf.TreeType.Wolf_Idle, this);

        while (Application.isPlaying && m_Tree != null)
        {
            yield return new WaitForSeconds(1 / m_Tree.Frequency);
            AIUpdate();

        }

    }

    void AIUpdate()
    {
        m_Tree.Tick();
    }

    public BehaveResult Tick(Behave.Runtime.Tree sender, bool init)
    {
        Debug.Log("Ticked Received by unhandled " +
        (BLWolf.IsAction(sender.ActiveID) ? "Action " :
        "Decorator ") +
        " ... " + (BLWolf.IsAction(sender.ActiveID) ?
        ((BLWolf.ActionType)sender.ActiveID).ToString() :
        ((BLWolf.DecoratorType)sender.ActiveID).ToString()));
        return BehaveResult.Success;
    }

    public void Reset(Behave.Runtime.Tree sender)
    {

    }

    public int SelectTopPriority(Behave.Runtime.Tree sender, params int[] IDs)
    {
        return 0;

    }

    public BehaveResult TickEnemyNearAction(Behave.Runtime.Tree sender)
    {
        

        Debug.Log("Wolf is looking for enemies");
        if (GameObject.FindGameObjectWithTag("Enemy")!= null)
        {
           
            Debug.Log("Enemy Found! OH GOOOOOD");

            return BehaveResult.Success;
        }
        else
        {
            Debug.Log("No Enemy Found!");

            return BehaveResult.Failure;
        }

    }

    public BehaveResult TickTooWoundedAction(Behave.Runtime.Tree sender)
    {

        Debug.Log("Wolf Checks if he is fine.");
        if (WS.Health<26)
        {
            Debug.Log("Wolfs wounds too great");
            return BehaveResult.Success;

        }
        else
        {
            Debug.Log("He is fine.");
            return BehaveResult.Failure;

        }



    }

    public BehaveResult TickAttackAction(Behave.Runtime.Tree sender) {

        Debug.Log("Wolf Attacks!");
        return BehaveResult.Success;

        
    }

    public BehaveResult TickHungryAction(Behave.Runtime.Tree sender) {
        if (WS.Hunger > 75)
        {
            Debug.Log("Wolfs Stomach is growling.");
            return BehaveResult.Success;

        }
        else
        {
            Debug.Log("Wolf is fine for now");
            return BehaveResult.Failure;
        }

    }


    public BehaveResult TickLieDownAction(Behave.Runtime.Tree sender)
    {
        Debug.Log("Wolf Lies Down for a bit");
        return BehaveResult.Success;
    }

    public BehaveResult TickHuntAction(Behave.Runtime.Tree sender)
    {
        Debug.Log("Wolf goes sniffing for pray");
        WS.Hunger = 0;
        return BehaveResult.Success;
    }

    public BehaveResult TickFleeAction(Behave.Runtime.Tree sender)
    {
        Debug.Log("Wolf flees");
        return BehaveResult.Success;
    }

    }
