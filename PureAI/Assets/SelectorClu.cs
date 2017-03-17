using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SelectorClu : MonoBehaviour {
    public List<GameObject> Qualifiers = new List<GameObject>();
    public float DefaultQualifierThreshhold;

    public void DefaultAction() {
        Debug.Log("Wandering what to do");

    }



    // Use this for initialization
    void Start () {
        


	}

    public IEnumerator TickAI() {
        while (true)
        {
            PingQualifiers();
            yield return new WaitForSeconds(2);
        }


    }

    public void PingQualifiers()
    {
        foreach (GameObject q in Qualifiers)
        {
            q.GetComponent<QualifierClu>().GetScore();
        }

        //List<BaseQualifier> SortedList = qualifiers.OrderBy(o => o.Score).ToList();
        List<GameObject> SortedList = Qualifiers.OrderBy(o => o.GetComponent<QualifierClu>().Score).ToList();
        SortedList.Reverse();
        if (SortedList[0].GetComponent<QualifierClu>().Score >= DefaultQualifierThreshhold)
        {
            SortedList[0].GetComponent<QualifierClu>().UseActions();

        }
        else
        {
            DefaultAction();
        }


    }




	// Update is called once per frame
	void Update () {
	
	}
}
