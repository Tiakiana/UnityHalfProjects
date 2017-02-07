using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {
    float health = 100;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    void OnMouseDown() {
        TakeDamage(10);

    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health<1)
        {
            gameObject.SendMessage("DestroyScr");
            Destroy(gameObject.GetComponent<NavMeshAgent>());
            Destroy(this);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x + 90, transform.eulerAngles.y, transform.eulerAngles.z);
            DestroyObject(gameObject,10);   
        }

    }
}
