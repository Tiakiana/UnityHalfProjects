using UnityEngine;
using System.Collections;

public class EnemyScr : MonoBehaviour {
    public float health = 5;
    public float SightRange, Speed;
    public GameObject Target, Player;

    // Use this for initialization
    void Start () {
        StartCoroutine("Detection");
        StartCoroutine("Shambling");
        Player = PlayerScr.PlayerSing.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (Target != null)
        {
        Vector3 diff = Target.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        }
    }

    IEnumerator Shambling()
    {
        while (true)
        {
            if (Target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed);
            }
            yield return null;
        }

    }


    public void TakeDamage(float damage) {
        health -= damage;
        if (health<=0)
        {
            CanvasController.CvsCtrl.SetKillAnimation();
            Destroy(gameObject);
        }
    }

    IEnumerator Detection()
    {

        while (true)
        {
            if (Target != null && Vector2.Distance(transform.position, Target.transform.position)>SightRange)
            {
                Target = null;
            }

            if (Target == null)
            {
                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, SightRange,8);
                float bestDist = 9999;
                int index = 999;
              //  Debug.Log(cols.Length);
                if (cols.Length > 0)
                {
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (Vector2.Distance(transform.position, cols[i].transform.position) < bestDist)
                        {
                            bestDist = Vector2.Distance(transform.position, cols[i].transform.position);
                            index = i;
                        }
                    }
                    Target = cols[index].gameObject;
                }
                else
                {
                    if (Player != null)
                    {
                        Target = Player;
                    }
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

}
