using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



abstract class Selector
{

    //Den standard værdi, de andre qualifiers skal matche for at det ikke bliver Default Action der bliver kørt.
    public float DefaultQualifierThreshhold { get; set; }

    private List<BaseQualifier> qualifiers = new List<BaseQualifier>();

    //Default actionen som kører, hvis ikke de andre qualifiers kommer over minimumsgrænsen.
    public void DefaultAction()
    {

    }


    public void AddQualifier(BaseQualifier q)
    {
        qualifiers.Add(q);
    }

    //Går slavisk igennem de forskellige qualifiers og ser på deres værdier,
    //de sorteres efter værdi og den qualifier med højest point vinder, og får udført sin handling.

    public void PingQualifiers()
    {
        foreach (BaseQualifier q in qualifiers)
        {
            q.PingScorers();
        }
        List<BaseQualifier> SortedList = qualifiers.OrderBy(o => o.Score).ToList();
        SortedList.Reverse();
        if (SortedList[0].Score >= DefaultQualifierThreshhold)
        {
            SortedList[0].UseActions();

        }
        else
        {
            DefaultAction();
        }


    }
}

