using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour {
    public const int maxHealth = 100;
    public GameObject DeathScreen;

    [SyncVar]
    public int currentHealth = maxHealth;

    public void TakeDamage(int dam) {
        if (!isLocalPlayer)
        {
            return;
        }
        currentHealth -= dam;
        if (currentHealth<= 0)
        {
            DeathScreen.SetActive(true);
        }
    }
	void Start () {
        DeathScreen = GameManager.GMInst.PanelOfDeath;
	}
	
	void Update () {
	
	}

    /*
     * public const int maxHealth = 100;

    [SyncVar]
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }
        
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }
     * 
     * 
     * */


}
