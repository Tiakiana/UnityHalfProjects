using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Xml;


/*

    Løsningen på, hvordan man fjerner fra en liste, mens man ittererer igennem den.
for (int i = safePendingList.Count - 1; i >= 0; i--)
{
    // some code
    // safePendingList.RemoveAt(i);
}
    */

public class CombatController : MonoBehaviour {
    public List<GameObject> combajim = new List<GameObject>();
    public List<ICombatant> Combatants = new List<ICombatant>();
    public static CombatController ComCtrl;
    //public List<ICombatant> Enemies = new List<ICombatant>();
    public void SetCombatantsAndStartCombat(List<GameObject> combatantlist )
    {
        combajim = combatantlist;
        List<ICombatant> combatan = new List<ICombatant>();
        foreach (GameObject o in combajim)
        {
            combatan.Add(o.GetComponent<ICombatant>());
        }

        StartCoroutine("CombatEncounter", combatan);
        

    }

    public IEnumerator CombatEncounter(List<ICombatant>  ls)
    {
        Combatants = ls;
        bool stillFighting = true;
        //AAAAAAAD GRIM GRIM LØSNING! FORSVIND!
        yield return  new WaitForSeconds(1);

        while (stillFighting)
        {
            for (int i = Combatants.Count-1; i >= 0; i--)
            {
                Combatants[i].SetCurrentAtkSpeed();

            }

            /*

            bool combatQueryEnem =
                (from combatant in Combatants
                    where combatant.GetTag() == "Player"
                    select combatant).Any();
            
            bool combatQueryPlay = (
                from combatant in Combatants
                where combatant.GetTag() == "Enemy"
                select combatant).Any();
                */

            for (int i = Combatants.Count - 1; i >= 0; i--)
            {
                if (stillFighting && Combatants[i].MakeAttack(Combatants))
                {
                    List<ICombatant> enem = new List<ICombatant>();
                    List<ICombatant> fren = new List<ICombatant>();

                    foreach (ICombatant combatant in Combatants)
                    {
                        if (combatant.GetTag() == "Player")
                        {
                            fren.Add(combatant);
                        }
                        else
                        {
                            enem.Add(combatant);
                        }
                    }
                    
                    if (fren.Count > 0 && enem.Count <= 0)
                    {
                        stillFighting = false;
                        // Good guys won
                        OnEndCombat();
                        Debug.Log("By The Crunch We've Won!");
                        break;
                    }
                    if (enem.Count > 0 && fren.Count <= 0)
                    {

                        stillFighting = false;
                        OnEndCombat();
                        Debug.Log("Dino Is of winnage");
                        break;
                        // Badguys Won
                    }
                    
                    /*
                    if (combatQueryPlay && !combatQueryEnem)
                    {
                        stillFighting = false;
                        // Good guys won
                        OnEndCombat();
                        Debug.Log("By The Crunch We've Won!");
                        break;
                    }
                    if (!combatQueryPlay && combatQueryEnem)
                    {
                        stillFighting = false;
                        OnEndCombat();
                        Debug.Log("Dino Is of winnage");
                        break;
                        // Badguys Won
                    }

                    */
                }

            }
         
            

            yield return null;
        }

        // Tell encountermanager to start cracking again.
    }

    /*
    public bool CheckForEndOfCombat()
    {

    }
    */
    public delegate void EndCombat();

    public event EndCombat OnEndCombat;

    void Awake()
    {
        ComCtrl = this;
    }

    void Start ()
    {
      
    }
	
	
	void Update () {
	
	}


    public void RemoveMe(GameObject me)
    {
        combajim.Remove(me);
        Combatants.Remove(me.GetComponent<ICombatant>());
    }

}
