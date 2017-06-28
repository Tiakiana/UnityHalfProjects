using UnityEngine;
using System.Collections;

public class FighterCtrl : MonoBehaviour {
    public GameObject Target, Bullet, Muzzle;
    public float CoolDownTime;
    float currentCoolDown;
    public bool Almost, Built;
    public Sprite[] sprites = new Sprite[3];
    public SteelTileScr Tile;
    public float Range = 10;
    public float damage = 1;
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        StartCoroutine("Placed");
        LevelScript.LvlScr.Fighters.Add(gameObject);
	}

    IEnumerator Placed() {
        yield return new WaitForSeconds(2);
        Almost = true;
        GetComponent<SpriteRenderer>().sprite = sprites[1];
        StartCoroutine("almost");

    }

    IEnumerator almost()
    {
        yield return new WaitForSeconds(2);
        Almost = false;
        Built = true;
        GetComponent<SpriteRenderer>().sprite = sprites[2];
        StartCoroutine("Detection");

    }

    IEnumerator Detection()
    {
        while (true)
        {
            if (Target == null)
            {

                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, Range);
                foreach (Collider2D item in cols)
                {
                    if (item.gameObject.tag == "Enemy")
                    {
                        Target = item.gameObject;
                        CanvasController.CvsCtrl.SetSpottedAnimation();
                    }
                }
            }
            else
            {
                if (Vector2.Distance(Target.transform.position, transform.position)>Range)
                {
                    Target = null;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }

    }

    public void Shoot() {
        GameObject go = Instantiate(Bullet, Muzzle.transform.position, transform.rotation) as GameObject;
        go.GetComponent<BulletScr>().damage = damage;
        GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Built && Target!=null )
        {


            Vector3 diff = Target.transform.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            if (CoolDownTime <= currentCoolDown)
            {
                Shoot();
                currentCoolDown = 0;
            }
            currentCoolDown += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        //Debug.Log("Hit enemy or something");
        if (col.gameObject.tag == "Enemy")
        {
            DestroyFighter();
        }

    }

    public void DestroyFighter() {
        CanvasController.CvsCtrl.SetDieAnimation();
        Tile.Occupied = false;
        LevelScript.LvlScr.Fighters.Remove(gameObject);
        Destroy(gameObject);
    }

    public void StartBlinking() {
        StartCoroutine("Blinking");

    }

    IEnumerator Blinking() {

        for (int i = 0; i < 5; i++)
        {

            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);

        }

        GetComponent<SpriteRenderer>().enabled = true;


    }

}
