using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bullet : NetworkBehaviour {
    public GameObject PlayerShot;
	void Start () {
        NetworkServer.Spawn(gameObject);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player" && col.gameObject != PlayerShot)
        {
              

            Debug.Log("Jakob");
            col.gameObject.SendMessage("TakeDamage",10);
        }
    }
	void Update () {
	
	}
}
