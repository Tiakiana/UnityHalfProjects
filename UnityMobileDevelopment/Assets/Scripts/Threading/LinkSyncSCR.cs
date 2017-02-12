using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;
using SharpConnect;
using System.Security.Permissions;

public class LinkSyncSCR : MonoBehaviour
{
    public Connector test = new Connector();
    string lastMessage;
    public Transform PlayerCoord;
    public InputField ipf;

    void Start()
    {
        Debug.Log(test.fnConnectResult("localhost", 10000,"Jakob"));
     
        if (test.res != "")
        {
            Debug.Log(test.res);
        }

    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space key was pressed");
            test.fnPacketTest("space key was pressed");
        }

        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("escape key was pressed");
            test.fnPacketTest("escape key was pressed");
        }
        if (test.strMessage != "JOIN")
        {
            if (test.res != lastMessage)
            {
                Debug.Log(test.res);
                lastMessage = test.res;
            }
        }
   //     test.fnPacketTest(PlayerCoord.position[0] + "," + PlayerCoord.position[1] + "," + PlayerCoord.position[2]);
    }

    public void SendMsg()
    {
        //test.fnPacketTest(ipf.text);
  test.SendData("Hej med dig");
    }

    void OnApplicationQuit()
    {
        try { test.fnDisconnect(); }
        catch { }
    }
}
