using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnScript : MonoBehaviour
{
    public GameObject jobContainer;
    public UseJob MyJob;
    public InputField ipf;
	// Use this for initialization
	void Start ()
	{
	    MyJob = jobContainer.GetComponent<UseJob>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Send()
    {
        if (MyJob!=null && ipf != null)
        {
            MyJob.myJob.SendRequest(ipf.text);
            ipf.text = "";
        }

    }

}
