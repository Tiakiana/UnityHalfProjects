﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public GameObject EscMenu, TravelMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("escape"))
        {
            if (EscMenu.active)
            {
                EscMenu.SetActive(false);

            }
            else
            {

            EscMenu.SetActive(true);
            }

            
        }
	}
}
