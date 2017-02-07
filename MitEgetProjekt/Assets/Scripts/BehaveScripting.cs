using UnityEngine;
using System.Collections;
using Behave.Runtime;

public class BehaveScripting : MonoBehaviour, IAgent
{

    Behave.Runtime.Tree m_Tree;
    
    //Dette er grundstenen i Behave. I stedet for BLNewBehaveLibrary, skriver man sit eget navn





    IEnumerator Start()
    {
        m_Tree = BLNewBehaveLibrary0.InstantiateTree(
            BLNewBehaveLibrary0.TreeType.Collection1_NewTree1, this);
       
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
        (BLNewBehaveLibrary0.IsAction(sender.ActiveID) ? "Action " :
        "Decorator ") +
        " ... " + (BLNewBehaveLibrary0.IsAction(sender.ActiveID) ?
        ((BLNewBehaveLibrary0.ActionType)sender.ActiveID).ToString() :
        ((BLNewBehaveLibrary0.DecoratorType)sender.ActiveID).ToString()));
        return BehaveResult.Success;
    }

    public void Reset(Behave.Runtime.Tree sender)
    {

    }

    public int SelectTopPriority(Behave.Runtime.Tree sender, params int[] IDs)
    {
        return 0;

    }

    public BehaveResult TickLeafAction(Behave.Runtime.Tree sender)
    {
        Debug.Log("FokYeah Dit Behaviør tree virker");

        return BehaveResult.Success;
    }
}
