using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Udregning : MonoBehaviour
{
    public Toggle Daidoji;
    public Dropdown EarthRing;
    public Dropdown Honour;
    public InputField Damage;
    public Text outputField;
    public int DamageCounter;
	// Use this for initialization
	void Start () {
	
	}
	



	void Update () {
	
	}

    public void SetDamage(int i)
    {
        DamageCounter += i;
        Damage.text = DamageCounter.ToString();
    }

    public void ResetDamage()
    {
        DamageCounter = 0;
        Damage.text = "";
    }


    public void MakeUdregning()
    {
        if (DamageCounter>0)
        {
            

            bool daidoji = Daidoji.isOn;
            int honour = Honour.value + 2;
            int earthring = EarthRing.value + 2;
            int damage = Int32.Parse(Damage.text);
            if (daidoji)
            {
                outputField.text = "TN +" + GetPenalty(earthring, damage, honour);

            }
            else
            {
                outputField.text = "TN +" + GetPenalty(earthring, damage);

            }
            Damage.text = "";
            Honour.value = 4;
            EarthRing.value = 0;
            DamageCounter = 0;
            Daidoji.isOn = false;

        }


    }


    public int GetPenalty(int earthRing, int damage)
    {

        int healthy = earthRing * 5 + earthRing * 2 * 0;
        int nicked = earthRing * 5 + earthRing * 2 * 1;
        int grazed = earthRing * 5 + earthRing * 2 * 2;
        int hurt = earthRing * 5 + earthRing * 2 * 3;
        int injured = earthRing * 5 + earthRing * 2 * 4;
        int crippled = earthRing * 5 + earthRing * 2 * 5;

        if (damage < healthy)
        {
            return 0;
        }
        if (damage < nicked)
        {
            return 3;
        }
        if (damage < grazed)
        {
            return 5;
        }
        if (damage < hurt)
        {
            return 10;
        }
        if (damage < injured)
        {
            return 20;
        }
        if (damage < crippled)
        {
            return 40;
        }
        else
        {
            return 888;
        }



    }


    public int GetPenalty(int earthRing, int damage, int honour)
    {
        int honourAdjust = -4 + honour;
        if (honourAdjust<1)
        {
            honourAdjust = 1;
        }

        int healthy = earthRing * 5 + earthRing * 2 * 0 + honourAdjust;
        int nicked = earthRing * 5 + earthRing * 2 * 1 + honourAdjust;
        int grazed = earthRing * 5 + earthRing * 2 * 2 + honourAdjust;
        int hurt = earthRing * 5 + earthRing * 2 * 3 + honourAdjust;
        int injured = earthRing * 5 + earthRing * 2 * 4 + honourAdjust;
        int crippled = earthRing * 5 + earthRing * 2 * 5+ honourAdjust;



        if (damage < healthy)
        {
            return 0;
        }
        if (damage < nicked)
        {
            return 3;
        }
        if (damage < grazed)
        {
            return 5;
        }
        if (damage < hurt)
        {
            return 10;
        }
        if (damage < injured)
        {
            return 20;
        }
        if (damage < crippled)
        {
            return 40;
        }
        else
        {
            return 888;
        }



    }

}
