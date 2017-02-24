using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkedMovement : NetworkBehaviour {
    


	void Start () {
	
	}
	
	
	void Update () {

        Debug.Log("What what" + isLocalPlayer);

    if (isLocalPlayer)
	    {
	        if (Input.GetKey("a"))
	        {
	            transform.position -= transform.right;
	        }
	    }
	}
}
