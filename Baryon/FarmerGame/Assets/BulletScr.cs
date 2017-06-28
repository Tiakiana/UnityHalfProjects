using UnityEngine;
using System.Collections;

public class BulletScr : MonoBehaviour {
    public float damage;
	// Use this for initialization
	void Start () {
        Destroy(gameObject,2);
        Physics2D.IgnoreLayerCollision(8,8,true);
     
	}

  
	// Update is called once per frame
	void Update () {
        transform.position += transform.up * 0.15f;
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.SendMessage("TakeDamage",damage);
        }
        Destroy(gameObject);
    }


}
