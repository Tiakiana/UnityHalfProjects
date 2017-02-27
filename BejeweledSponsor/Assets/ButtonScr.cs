using UnityEngine;
using System.Collections;

public class ButtonScr : MonoBehaviour {
    public Vector2 Coordinates;
    public int Item;
    public bool Inventory = false;
	// Use this for initialization
	void Start () {
        OnClick += SendData;
	}

    void OnDisable() {
        OnClick -= SendData;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public delegate ButtonScr Clicker();

    public event Clicker OnClick;

    public void Clicked() {
        OnClick.Invoke();

    }
    public ButtonScr PlaceGem() {

        return this;
    }

    public ButtonScr SendData() {
        Debug.Log("Clicked" + Item);
        if (Inventory)
        {
            GameManager.GmInst.Held = this;
            Debug.Log("This is now held " + this.Item);
        }
        else
        {
            if (GameManager.GmInst.Held!= null)
            {
                GameManager.GmInst.EvilMove(this);

            }
        }
        return this;

    }


}
