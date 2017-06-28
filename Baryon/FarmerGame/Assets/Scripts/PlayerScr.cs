using UnityEngine;
using System.Collections;

public class PlayerScr : MonoBehaviour
{
    public GameObject Target;
    public SteelTileScr TileTarget;
    public static PlayerScr PlayerSing;
    public float Speed;
    public int Action = 0;
    public bool moving = false;
    public bool ActionActive = false;


    void Awake()
    {
        PlayerSing = this;
    }

    void Start()
    {
        StartCoroutine("Walking");
        Physics2D.IgnoreLayerCollision(8, 8, true);
    }

    IEnumerator Walking()
    {

        while (true)
        {
            if (Vector2.Distance(transform.position, Target.transform.position) > 0.1)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed);

            }
            else
            {
                switch (Action)
                {
                    case 0:
                        moving = false;
                        break;
                    case 1:
                        if (moving)
                        {
                            moving = false;
                            TileTarget.SowBacon();
                            Action = 0;
                            CanvasController.CvsCtrl.SetNothing();

                        }

                        break;

                    case 2:
                        if (moving)
                        {
                            moving = false;
                            TileTarget.CreateFighter();
                            Action = 0;
                            CanvasController.CvsCtrl.SetNothing();

                        }
                        break;

                    default:
                        break;
                }
            }
            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            CanvasController.CvsCtrl.YouDied();
        }
      
    }

    void Update()
    {
        Vector3 diff = Target.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        if (Input.GetKeyDown("1"))
        {
            if (moving)
            {
                Target.transform.position = transform.position;
            }
            moving = false;
            Action = 1;
            CanvasController.CvsCtrl.SetBacon();

        }
        if (Input.GetKeyDown("2"))
        {
            if (moving)
            {
                Target.transform.position = transform.position;
            }
            moving = false;
            Action = 2;
            CanvasController.CvsCtrl.SetHat();


        }
        if (Input.GetKeyDown("3"))
        {
            if (CanvasController.CvsCtrl.PlayerPoints>= 2000)
            {
                CanvasController.CvsCtrl.PlayerPoints -= 2000;
                LevelScript.LvlScr.UpgradeFighters();
                GetComponent<AudioSource>().Play();
            }
        }

    }
}
