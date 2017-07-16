using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Market : MonoBehaviour
{
    public string MarketName;
    public int marketClass;
    public int toll;
    public int merchants;
    public int loads;
    public double customs;
    public int xpos, ypos;
    public Market WestMarket, NorthMarket, EastMarket, Southmarket;



    public enum TerrainTypes {
        SeaCoast = 0,
        Lake,
        River,
        RainForest,
        Savanna,
        Desert,
        Steppe,
        Scrub,
        Grassland,
        DecidousForest,
        Taiga,
        Tundra,
        Plains,
        Hills,
        Mountains

    }
    public TerrainTypes Terrain;

    public static Market Mrkt;
    public List<GameObject> Merchants = new List<GameObject>();

    public GameObject Prefab;

    #region Prices

    private double[] StandardPrices = new double[29] {
10,
50,
50,
60,
100,
100,
100,
100,
150,
150,
200,
200,
200,
200,
200,
200,
225,
250,
400,
80,
200,
3000,
2000,
1000,
1000,
500,
600,
800,
300,
};

    public double[] Prices = new double[29] {10, 50,50, 60, 100, 100,
100,
100,
150,
150,
200,
200,
200,
200,
200,
200,
225,
250,
400,
80,
200,
3000,
2000,
1000,
1000,
500,
600,
800,
300,
};
    #endregion
    public double[] Demand = new double[29];
    public double[] AgeDemands = new double[29];
    public double[] TerrainDemands = new double[29];



    #region Names On goods
    public string[] namesofGoods = new string[29]{


"Grain, Vegetables",
"Fish, Preserved",
"Wood, Common",
"Sheep",
"Salt",
"Beer, Ale",
"Oil, Lamp",
"Textiles",
"Hides, Fur",
"Tea, Coffee",
"Metals, Common",
"Meats, Preserved",
"Cloth" ,
"Wine, Spirits"  ,
"Pottery" ,
"Tools",
"Arms, Armor",
"Dye & Pigments",
"Glassware",
"Horse, Yak",
"Warhorse",
"Gems",
"Silk",
"Books, Rare",
"Porcelain,Fine",
"Furs, Rare",
"Precious Metals",
"Spices",
"Monster Parts"
     };
    #endregion


    public List<int> goods = new List<int>();
    public List<string> names = new List<string>();
    public List<double> prix = new List<double>();

    private void Awake()
    {
        Mrkt = this;
    }

    public void RandomizeDemands()
    {
        for (int i = 0; i <= this.Demand.Length - 1; i++)
        {
            this.Demand[i] = Random.Range(-2, 3);
        }

    }

    public void RandomizeCustoms()
    {
        customs = Random.Range(1, 11) + Random.Range(1, 11);

    }

    public void SetupMarket(string name, int marketClss, int terrainType)
    {
        RandomizeDemands();
        MarketName = name;
        marketClass = marketClss;
        RandomizeCustoms();
        SetTerrainDemands(terrainType);
        //Debug.Log("Customs are: " + customs);

    }


    public void GoToMarket(int marketclass) {

        for (int i = Merchants.Count - 1; i >= 0; i--)
        {
            Destroy(Merchants[i]);
        }
        MarketDerivatives(marketclass);
        StandardPriceVariation();
        Goods4Sale();
        Player.PlayerInst.Money -= toll;
    }



    // Use this for initialization
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("s"))
        {
        SetTerrainDemands(0);

        }

        if (Input.GetKeyUp("1"))
        {
            GoToMarket(3);
        }


    }
    //0 = sea/ coast
    public void SetTerrainDemands(int terrain) {

        TerrainDemands = TextIO.TxtIO.GetTerrainDemands(terrain);

        for (int y = 0; y < 29; y++)
        {
         //   Debug.Log(TextIO.TxtIO.TerrainTable[terrain, y]);

             double ix = TextIO.TxtIO.TerrainTable[terrain, y];
            TerrainDemands[y] = ix;


        }
        foreach (double item in TerrainDemands)
        {
          //  Debug.Log(item);
        }

    }

    //
    public void StandardPriceVariation()
    {
        // Her nulstilles priserne

        for (int i = 0; i <= Prices.Length - 1; i++)
        {
            this.Prices[i] = this.StandardPrices[i];
        }

        //her kommer den nye pris :)

        for (int i = 0; i <= Prices.Length - 1; i++)
        {
            double res = (Random.Range(-2, 3) +  this.Demand[i] + TerrainDemands[i]) * 0.1 + 1;
            this.Prices[i] = this.Prices[i] * res;
        }



    }

    //Så skal vi bruge en constructor til når vi laver et nyt marked.
    //Desværre gør den det at den, hver gang man åbner programmet, laver en ny provins, 
    //hvorfor supply og demand ændrer sig voldsomt.



    // Markedsklasse, Navn, Toll, Loads og Merchantantal

    public void MarketDerivatives(int market)
    {

        this.marketClass = market;
        if (this.marketClass == 6)
        {
            this.toll = Random.Range(1, 4);
            this.merchants = Random.Range(1, 4) - 1;
            this.loads = Random.Range(1, 3);

        }

        if (this.marketClass == 5)
        {
            this.toll = Random.Range(1, 7);
            this.merchants = Random.Range(1, 5) - 1;
            this.loads = Random.Range(1, 5);
        }

        if (this.marketClass == 4)
        {
            this.toll = Random.Range(1, 7) + 3;
            this.merchants = Random.Range(1, 5);
            this.loads = Random.Range(1, 5) + Random.Range(1, 5);

        }

        if (this.marketClass == 3)
        {
            this.toll = Random.Range(1, 8) + 5;
            this.merchants = Random.Range(1, 5) + Random.Range(1, 5);
            this.loads = Random.Range(1, 5) + Random.Range(1, 5) + Random.Range(1, 5);

        }

        if (this.marketClass == 2)
        {
            this.toll = Random.Range(1, 11) + 10;
            this.merchants = Random.Range(1, 5) + Random.Range(1, 5) + 1;
            this.loads = Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7);

        }

        if (this.marketClass == 1)
        {
            this.toll = Random.Range(1, 7) + 15;
            this.merchants = Random.Range(1, 7) + Random.Range(1, 7) + 2;
            this.loads = Random.Range(1, 9) + Random.Range(1, 9) + Random.Range(1, 9) + Random.Range(1, 9) + Random.Range(1, 9) + Random.Range(1, 9);

        }
    }


    //Hvilke goder er til salg? Vi kan tage til at starte med en flad chance for hver af goderne.

    public void CreateNeighbour(int direction) {
        if (direction == 0)
        {
            
        }
    }

    public void Goods4Sale()
    {
        this.goods.Clear();
        this.prix.Clear();
        this.names.Clear();

        for (int i = this.merchants; i >= 0; i--)
        {
            this.goods.Add(Random.Range(0, 29));
        }
        int yh = 0;
        int ex = 0;
        // Noget går galt her! Fiks det!
        for (int i = 0; i <= this.merchants; i++)
        {
            GameObject Merch = (GameObject)Instantiate(Prefab, new Vector3(ex * 3, yh * 3 + 1), Quaternion.identity);
            Merchants.Add(Merch);
            string s = "" + namesofGoods[goods[i]] + "\n" + Prices[goods[i]];
            Good g = new Good();
            g.Name = namesofGoods[goods[i]];
            g.Price = Prices[goods[i]];
            Merch.GetComponent<Merchant>().goods = g;
            Merch.GetComponent<Merchant>().SetText(s, loads, this);


            this.prix.Insert(i, this.Prices[this.goods[i]]);
            this.names.Insert(i, this.namesofGoods[this.goods[i]]);
            ex++;
            if (ex == 8)
            {
                ex = 0;
                yh--;
            }

        }

    }












    //goods.Clear();
    //Random rnd = new Random();

    //goods.Add(rnd.Next(0, 29));

    //names.Insert(0,namesofGoods[goods[0]]);






}
