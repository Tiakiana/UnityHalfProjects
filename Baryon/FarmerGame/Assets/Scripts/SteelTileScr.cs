using UnityEngine;
using System.Collections;

public class SteelTileScr : MonoBehaviour {
    public bool Sown, Grown, Occupied;
    public Sprite[] sprites = new Sprite[3];
    public GameObject Fighter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Sown)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        else if (Grown)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[2];

        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];

        }
    }
    public void SowBacon() {
        if (!Sown && !Grown && !Occupied )
        {
            Sown = true;
            StartCoroutine("GrowBacon");
        }

    }
    public void CreateFighter() {
        if (!Occupied && CanvasController.CvsCtrl.PlayerPoints>199)
        {

            GameObject go = Instantiate(Fighter, transform.position, transform.rotation) as GameObject;
            go.GetComponent<FighterCtrl>().Tile = this;
            Occupied = true;
            CanvasController.CvsCtrl.PlayerPoints -= 200;
        }

    }
    IEnumerator GrowBacon() {
        yield return new WaitForSeconds(8);
        Sown = false;
        Grown = true;
    }
    public void HarvestBacon() {
        if (Grown)
        {
            Grown = false;
            CanvasController.CvsCtrl.PlayerPoints+= 50;
        }
    }

    void SetMoving() {

        PlayerScr.PlayerSing.Target.transform.position = transform.position;
        PlayerScr.PlayerSing.TileTarget = this;
        PlayerScr.PlayerSing.moving = true;
    }

    void OnMouseDown() {
        // Set player order to move here.
        SetMoving();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
        {
            
                HarvestBacon();
                
            
        }
        if (col.gameObject.tag == "Enemy")
        {
            Sown = false;
            Grown = false;
        }

    }
}
