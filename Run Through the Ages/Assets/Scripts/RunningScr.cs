using UnityEngine;
using System.Collections;

public class RunningScr : MonoBehaviour
{
    public float minimumHop;
    public float maximumHop;
	// Use this for initialization
	void Start () {
       Physics2D.IgnoreLayerCollision(8,8,true);
	    EncounterController.EncCtrl.OnStep += Jump;
	}

    public void BeforeDeath()
    {
        EncounterController.EncCtrl.OnStep -= Jump;

    }

    public void Jump()
    {
        float force = Random.Range(minimumHop, maximumHop) * 10;

        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update () {
	    if (Input.GetKeyDown("e"))
	    {
	        float force = Random.Range(minimumHop, maximumHop)*10;

	        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*force,ForceMode2D.Impulse);
	    }

        if (Input.GetKeyDown("w"))
        {
	        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*10,ForceMode2D.Impulse);

        }
    }
}
