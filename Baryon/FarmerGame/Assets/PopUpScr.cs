using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopUpScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public void StartEnter() {
        GetComponent<Image>().enabled = true;
        StartCoroutine("Enter");

    }

    IEnumerator Enter() {
        transform.localScale = new Vector3(0, 0, 0);

        while (transform.localScale.x<1)
        {
            yield return new WaitForSeconds(0.0001f);
            transform.localScale += new Vector3(0.1f,0.1f,0.1f);

        }
        yield return new WaitForSeconds(1);
        while (transform.localScale.x>0)
        {

            yield return new WaitForSeconds(0.01f);
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }

        GetComponent<Image>().enabled = false;

    }
    // Update is called once per frame
    void Update () {
	
	}
}
