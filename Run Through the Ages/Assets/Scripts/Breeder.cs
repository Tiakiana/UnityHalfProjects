using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Breeder : MonoBehaviour
{
    public static Breeder BreederInstance;
    public List<Sprite> Sprites = new List<Sprite>();
    private Character hum1;
    private Character hum2;
    public int MutationChance;

    public void SetSpecimen(Character humi1, Character humi2)
    {
        hum1 = humi1;
        hum2 = humi2;
    }

    void Awake()
    {
        BreederInstance = this;
    }

    public void Breed()
    {

        if (hum1 != null && hum2 != null)
        {
            float[] dad = new float[hum1.GetStatLine().Length];
            float[] mom = new float[hum2.GetStatLine().Length];
            hum1.GetStatLine().CopyTo(dad,0);
            hum2.GetStatLine().CopyTo(mom,0);
            int noofchildren = Random.Range(1, 5);

            for (int y = 0; y < noofchildren; y++)
            {
                
            float[] statlinenew = new float[hum1.GetStatLine().Length];

            for (int i = 0; i < hum1.GetStatLine().Length; i++)
            {
                int x = Random.Range(1, 3);
                if (x== 1)
                {
                    statlinenew[i] = dad[i];


                }
                else
                {
                    statlinenew[i] = mom[i];

                }
            }
            GameObject go = new GameObject();
            go.AddComponent<Character>().CreateCharacter("Child", 1, true, statlinenew);
                go.AddComponent<SpriteRenderer>().sprite = Sprites[ Random.Range(0,4)];
                Vector3 pos = new Vector3(Random.Range(-7,8),Random.Range(-4,5));
                go.transform.position = pos;
                go.AddComponent<CavemanControl>().SetBreeder(this);
                go.AddComponent<BoxCollider>();

                int die = Random.Range(1, 101);
                if (die>= MutationChance)
                {
                    die = Random.Range(0, 5);
                    int chg = Random.Range(-1, 2);
                    go.GetComponent<Character>().ChangeStat(die,chg);



                }


                //  Debug.Log("Creating child");
                go.name = "Hooman# " + Random.Range(1, 10000) + " ";
                foreach (var VARIABLE in statlinenew)
                {
                    go.name += VARIABLE + " ";
                }
            }
        }
        OnBreed();

    }

    public Character GetCharacter1()
    {
        return hum1;
    }

    public void SetCharacter1(Character c)
    {
        hum1 = c;
        Debug.Log("Dad Is now: " + c.name);
        OnChange(hum1, hum2);

    }

    public Character GetCharacter2()
    {
        return hum2;

    }

    public void SetCharacter2(Character c)
    {
        hum2 = c;
        Debug.Log("Mom Is now: " + c.name);
        OnChange(hum1, hum2);
    }


    public delegate void Change(Character a, Character b);

    public event Change OnChange;


    public delegate void Breeding();
    // bruges af character klassen til at ælde menneskene.
    public event Breeding OnBreed;

    void Start () {
	
	}
	
	void Update () {
	
	}
}
