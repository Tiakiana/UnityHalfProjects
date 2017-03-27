using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIObserver : MonoBehaviour
{
    public Text Mom, Dad,Selected;

   

	// Use this for initialization
	void Start ()
	{
	    Breeder.BreederInstance.OnChange += UpdateTexts;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void UpdateTexts(Character mom, Character dad)
    {
        if (mom!= null)
        {
            Mom.text = "Mom: " + mom.name + "\n" + "Strength: " + mom.GetStatLine()[0] + "\n Stamina: " +
                       mom.GetStatLine()[1] + "\n Attack Speed: " + mom.GetStatLine()[2] + "\n Speed: " +
                       mom.GetStatLine()[3] + "\n Courage:" + mom.GetStatLine()[4];

        }

        if (dad!= null)
        {
            Dad.text = "Dad: " + dad.name + "\n" + "Strength: " + dad.GetStatLine()[0] + "\n Stamina: " +
                       dad.GetStatLine()[1] + "\n Attack Speed: " + dad.GetStatLine()[2] + "\n Speed: " +
                       dad.GetStatLine()[3] + "\n Courage:" + dad.GetStatLine()[4];

        }
        if (Selected !=null)
        {
            Selected.text = "";
            Selected.text = "Selected Guys:\n";
            if (GameController.GameCtrl.Cavemen.Count>0)
            {
                foreach (GameObject caveman in GameController.GameCtrl.Cavemen)
                {
                    Selected.text += caveman.gameObject.name + "\n";
                }
            }
        }

    }
}
