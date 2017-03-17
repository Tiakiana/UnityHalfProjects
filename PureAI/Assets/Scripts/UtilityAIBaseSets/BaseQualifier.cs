using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract class BaseQualifier
{


    private float score;
    public float Score { get { return score; } }




    public List<IAction> ActionsInterfaces = new List<IAction>();



    public List<IScorer> scorers = new List<IScorer>();

    public void AddAction(IAction iact)
    {
        ActionsInterfaces.Add(iact);

    }
    public void AddScorer(IScorer iscorer) {
        scorers.Add(iscorer);

    }
    public void SetScore(float i)
    {
        score = i;
    }


    //Giver qualifieren den endelige scorer som selectoren bliver færdig med.
    public void PingScorers()
    {
        foreach (IScorer item in scorers)
        {
            score += item.ReturnScore();
        }
    }






    public void UseActions()
    {
        foreach (IAction item in ActionsInterfaces)
        {
            item.Execute();
        }
    }


}
