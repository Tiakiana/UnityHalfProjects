using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraScript : NetworkBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {

        }
        if (GetComponent<NetworkView>().isMine)
        {
            GetComponent<Camera>().gameObject.SetActive(true);
        }
        else
        {
            GetComponent<Camera>().gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
