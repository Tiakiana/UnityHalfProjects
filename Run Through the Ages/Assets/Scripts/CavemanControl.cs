using UnityEngine;
using System.Collections;

public class CavemanControl : MonoBehaviour
{
    public Breeder Breedster;
    private Character character;
	// Use this for initialization
	void Start ()
	{
	    character = gameObject.GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        


      

    }

    public void SetBreeder(Breeder bred)
    {
        Breedster = bred;
    }

    public bool Right = false;
  void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        Breedster.SetCharacter1(character);

        if (Input.GetMouseButtonDown(1))
        Breedster.SetCharacter2(character);
      if (Input.GetMouseButtonDown(2))
      {
          GameController.GameCtrl.AddCaveman(gameObject);
            Breedster.SetCharacter1(character);
      }

    }


        /* void OnMouseDown()
        {
            if (Right)
            {
                Breedster.SetCharacter1(character);
                Right = false;
            }
            else
            {
                Breedster.SetCharacter2(character);
                Right = true;
            }



        }*/
    }
