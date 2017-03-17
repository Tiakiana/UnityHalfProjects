using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIScript : MonoBehaviour {

    // Use this for initialization
    Stats stats;
    List<Selector> selectors = new List<Selector>();
    void Start () {
        stats = GetComponent<Stats>();
        SelectorChooseBest sl = new SelectorChooseBest();
        selectors.Add(sl);
        WhatINEED wi = new WhatINEED();

        sl.AddQualifier(wi);

        StartCoroutine("AI");



	}

    IEnumerator AI() {

        while (true)
        {
            foreach (Selector item in selectors)
            {
                item.PingQualifiers();
            }
            yield return new WaitForSeconds(2);
        }

    }
	


}
