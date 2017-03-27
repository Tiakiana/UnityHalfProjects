using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScr : MonoBehaviour, ICombatant
{
    public float Speed, Attack, AttackSpeed, Courage, Health, CurrentAtkSpeed;
    public string Tag;
    public Encounter Encountered;
    private ICombatant target;
    public GameObject AnimationHit;

    /*
    Dette script skal have en animator på gameobjektet og en Camerashake script på sig for at virke ordentligt.

    */


    void Start () {
	
	}

   

   
    void Update()
    {
    }

    public void SetEncounter(Encounter e)
    {
        Encountered = e;
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public float GetAttack()
    {
       return Attack;
    }

    public float GetAttackSpeed()
    {
        return AttackSpeed;
    }

    public float GetCourage()
    {
       return Courage;
    }

    public float GetHealth()
    {
        return Health;
    }

    public float GetCurrentAtkSpeed()
    {
        return CurrentAtkSpeed;
    }

    public string GetTag()
    {
        return Tag;
    }

    public ICombatant GetTarget()
    {
        return target;
    }

    public void SetTarget(ICombatant combatant)
    {
        target = combatant;
    }
    //spørger om target'et er dødt :D
    public bool TakeDamage(float damage)
    {
        AnimationHit.GetComponent<Animator>().SetTrigger("GetHit");
        GetComponent<ScreenShake>().SetPosition();

        GetComponent<ScreenShake>().Shake();
        Health -= damage;
        if (Health <= 0)
        {
            CombatController.ComCtrl.RemoveMe(gameObject);
            Encountered.RemoveEnemy(gameObject);
            Destroy(gameObject);

            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetCurrentAtkSpeed()
    {
        CurrentAtkSpeed += AttackSpeed;
    }

    public void SetCurrentAtkSpeed(float AtkSpeed)
    {
        CurrentAtkSpeed = AtkSpeed;
    }

    public bool MakeAttack(List<ICombatant> ls)
    {
        bool result = false;
        if (CurrentAtkSpeed >= 100)
        {
            GetComponent<AudioSource>().Play();

            CurrentAtkSpeed -= 100;

            if (target == null)
            {
                foreach (ICombatant combatant in ls)
                {
                    if (combatant.GetTag() == "Player")
                    {
                        SetTarget(combatant);
                        break;
                    }

                }

            }
            if (target != null)
            {

                if (target.TakeDamage(Attack))
                {

                    result = true;
                }
                else result = false;

            }
            else
            {
                result = true;
                Debug.Log("Noone left to bang");
            }
        }
        target = null;
        return result;
    }
}
