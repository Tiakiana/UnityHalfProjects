using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Shoot : NetworkBehaviour {
    public float CoolDown;
    public Rigidbody projectile;
    public float speed;
    public GameObject muzzle;
	void Start () {
	
	}
    [Command]
    void CmdFire() {
        Rigidbody instantiatedProjectile = Instantiate(projectile, muzzle.transform.position, transform.rotation) as Rigidbody;
        instantiatedProjectile.GetComponent<Bullet>().PlayerShot = gameObject;
        NetworkServer.Spawn(instantiatedProjectile.gameObject);

        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        Destroy(instantiatedProjectile.gameObject, 3);


    }
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            CmdFire();
        }
    }
}
