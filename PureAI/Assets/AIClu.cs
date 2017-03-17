using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIClu : MonoBehaviour {
    public List<GameObject> Selectors = new List<GameObject>();
	// Use this for initialization
	void Start (){
        StartCoroutine("PingSelectors");
	}

    public IEnumerator PingSelectors() {
        while (true) {

            yield return new WaitForSeconds(3);
            foreach (GameObject item in Selectors)
            {
                item.GetComponent<SelectorClu>(). PingQualifiers();

            }
           

        }

    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
