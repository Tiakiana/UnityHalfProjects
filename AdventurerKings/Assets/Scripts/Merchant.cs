using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Merchant : MonoBehaviour {
    public TextMesh txt;
    public Good goods;
    Player player;
    int Loads;
  public  Market market;

    // Use this for initialization
    void Start()
    {
        player = Player.PlayerInst;
        List<Good> matches = player.Goods.Where(p => p.Name == goods.Name).ToList();


        if (matches != null && matches.Count > 0)
        {
            txt.text += "  -!!!-";

        }
        else
        {
 
        }
    }



    public void SetText(string s, int loads, Market mark) {
            txt.text = s;
        Loads = loads;
        market = mark;
       

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (player.Money>= goods.Price)
            {
                player.Money -= goods.Price;
                player.Goods.Add(goods);
                Loads--;
                if (Loads== 0)
                {
                    Market.Mrkt.Merchants.Remove(this.gameObject);

                    Destroy(gameObject);

                }

            }


        }
        if (Input.GetMouseButtonUp(1))
        {
            //List<Order> SortedList = objListOrder.OrderBy(o => o.OrderDate).ToList();
            List<Good> matches = player.Goods.Where(p => p.Name == goods.Name).ToList();
            

            if (matches != null && matches.Count>0)
            {

                player.Goods.Remove(matches[0]);
                double customsPrice = goods.Price * (market.customs/100);
                player.Money += goods.Price - customsPrice;

                Debug.Log("Sold for: " + (goods.Price - customsPrice) + ". Customs was " + customsPrice);
            }

        }
    }

    // Update is called once per frame
    void Update () {

    }
}
