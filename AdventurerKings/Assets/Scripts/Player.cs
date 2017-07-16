using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

public class Player : MonoBehaviour {

    public static Player PlayerInst;
    public List<Good> Goods = new List<Good>();
    public Text txt;
    private void Awake()
    {
        PlayerInst = this;
    }

    public double Money;

    void Start () {
  
    }

	// Update is called once per frame
	void Update () {
        //Debug.Log(Goods.Count);
        string s = "Money: " + Money + "\n";

        foreach (Good item in Goods)
        {
            s += item.Name + ":  " +  item.Price + "\n";
        }

        txt.text = s;
        
	}

    public void NewPlayer() {
        Money = 1000;
        Goods = new List<Good>();

    }

    public void LoadPlayer() {
        if (GetPlayer() != null)

        {
            PlayerInfo pi =  GetPlayer();
            Money = pi.playerMoney;
            Goods = pi.playerGoods;
        }

    }

    public void SavePlayer()
    {

        System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(PlayerInfo));

        var path = "PlayerSave" + ".xml";
        System.IO.FileStream file = System.IO.File.Create(path);
        PlayerInfo pi = new PlayerInfo(Goods,Money);
        writer.Serialize(file, pi);
        file.Close();

    }

public PlayerInfo GetPlayer()
    {
        System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(PlayerInfo));

        var path = "PlayerSave.xml";

        if (File.Exists(path))
        {
        System.IO.StreamReader file = new System.IO.StreamReader(path);
        PlayerInfo player = (PlayerInfo)reader.Deserialize(file);
        file.Close();
        return player;

        }
        else
        {
            return null;
        }


    }
    public class PlayerInfo{

        public double playerMoney;
        public List<Good> playerGoods;
        public PlayerInfo() { }

        public PlayerInfo(List<Good> goods, double money)
        {

            playerGoods = goods;
            playerMoney = money;
        }
    
    }


}
