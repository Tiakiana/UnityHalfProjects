using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour, ICombatant
{
    public string Name { get; set; }
    public int Age { get; set; }
    public bool Sex { get; set; }
    public string Tag;
    public bool Beginning;


    //strength, stamina, atkspeed, speed, courage, intelligence
    private float[] statLine;
    private float health { get; set; }
    private float currentAtkSpeed { get; set; }
    private ICombatant target { get; set; }
    /*
    public Character(string name, int age, bool sex, float[] statline)
    {
        Name = name;
        Age = age;
        Sex = sex;
        statLine = statline;
        health = statline[1];
        currentAtkSpeed = 0;
        target = null;


    }

        */
    /*
    public Character()
    {
        name = "Hooman #"+ Random.Range(1, 10000);
        Age = 1;
        statLine = new float[5];

        name += " (";
        for (int i = 0; i < statLine.Length; i++)
        {
            statLine[i] = Random.Range(1, 7);
            name += statLine[i] + ", ";
        }
        name += ")";
        health = statLine[1];
        currentAtkSpeed = 0;
        target = null;



    }
    */

    public string GetTag()
    {
        return Tag;
    }
    
    public void CreateCharacter(string name, int age, bool sex, float[] statline)
    {
        Name = name;
        Age = age;
        Sex = sex;
        statLine = statline;
        health = statline[1];
        currentAtkSpeed = 0;
        
        target = null;
    }
    //Hvis Beginning er sat til true giver det en random character fra start. Efter start kan man justere karakteren med ovenstående method.

    public void CreateRandomCharacter()
    {
        name = "Hooman #" + Random.Range(1, 10000);
        Age = 1;
        statLine = new float[5];

        name += " (";
        for (int i = 0; i < statLine.Length; i++)
        {
            statLine[i] = Random.Range(1, 4);
            name += statLine[i] + ", ";
        }
        name += ")";
        health = statLine[1];
        currentAtkSpeed = 0;
        target = null;
        statLine[4] = 75;
    }

    public void ChangeStat(int index, int chg)
    {
        statLine[index] += chg;
    }

    public float[] GetStatLine()
    {
        
        return statLine;
    }

    public float GetSpeed()
    {
        return statLine[3];
    }

    public float GetAttack()
    {
        return statLine[0];

    }

    public void FlowOfTime()
    {
        Age++;
        if (Age>=4)
        {
            GameController.GameCtrl.Cavemen.Remove(gameObject);
            Destroy(gameObject);
           
        }

    }

    public float GetAttackSpeed()
    {
        return statLine[2];

    }

    public float GetCourage()
    {
        return statLine[4];

    }

    public float GetHealth()
    {
        return health;
    }

    public float GetCurrentAtkSpeed()
    {
     return currentAtkSpeed;
    }

    public ICombatant GetTarget()
    {
        return target;
    }

    public void SetTarget(ICombatant combatant)
    {
        target = combatant;
    }

    public bool TakeDamage(float damage)
    {
        GetComponent<ScreenShake>().SetPosition();

        GetComponent<ScreenShake>().Shake();
        health -= damage;
        if (gameObject != null && health <= 0)
        {
            GetComponent<RunningScr>().BeforeDeath();
            Destroy(gameObject);

            CombatController.ComCtrl.RemoveMe(gameObject);
            EncounterController.EncCtrl.RemoveMe(gameObject);
            GameController.GameCtrl.RemoveCaveman(gameObject);

            return true;
        }
        else
        {
            return false;

        }
    }

    public void SetCurrentAtkSpeed()
    {
        currentAtkSpeed += statLine[2];
    }

    public void SetCurrentAtkSpeed(float AtkSpeed)
    {
        currentAtkSpeed = AtkSpeed;
    }

    public bool MakeAttack(List<ICombatant> combatants)
    {
        bool result = false;
        if (currentAtkSpeed >= 100)
        {
            GetComponent<AudioSource>().Play();

            currentAtkSpeed -= 100;

            if (target == null)
            {
                foreach (ICombatant combatant in combatants)
                {
                    if (combatant.GetTag() == "Enemy")
                    {
                        SetTarget(combatant);
                        break;
                    }

                }

            }
            if (target != null)
            {

                if (target.TakeDamage(statLine[0]))
                {
                    result = true;
                }
                else result = false;

            }
            else
            {
                result = false;
                Debug.Log("Noone left to bang");
            }
        }
        target = null;
        return result;
    }

    public void SpawnCaveguysFromPrevious()
    {

    }











    void Start()
    {
        if (Beginning)
        {
            CreateRandomCharacter();
        }

        Breeder.BreederInstance.OnBreed += FlowOfTime;
    }


    void Update()
    {
        //Debug.Log(statLine[4]);
    }


}
