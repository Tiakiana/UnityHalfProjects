using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour {
    public int health = 100;

    public void TakeDamage(int dam) {
        if (!isLocalPlayer)
        {
            return;
        }
        health -= dam;
        if (health<= 0)
        {
            Destroy(gameObject);
        }
    }
	void Start () {
	
	}
	
	void Update () {
	
	}
}
