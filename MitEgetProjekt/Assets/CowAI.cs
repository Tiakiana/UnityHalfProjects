using UnityEngine;
using System.Collections;

public class CowAI : MonoBehaviour {

    public float hunger = 0;
    public float tiredness = 0; 
    public float mate = 0;
    public float threshhold = 60;
    public bool ReadyForNewAi = true;
    public GameObject Cow;

    IEnumerator AIStart() {
       

        while (true)
        {

            if (ReadyForNewAi)
            {
                if (hunger > threshhold)
                {

                    StartGrazing();

                }
                else if (tiredness > threshhold)
                {
                    StartSleeping();
                }
                else if (mate > threshhold + 20 )
                {
                    StartMating();
                }
                else
                {
                    StartWandering();
                }

            }

            yield return new WaitForSeconds(1);



        }

    }


    IEnumerator Needs() {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            hunger += Random.Range(0, 4);
            tiredness += Random.Range(0, 4);
            mate += Random.Range(0, 3);


        }

    }

    IEnumerator Grazing() {
        while (hunger>10)
        {
            hunger -= 10;
            yield return new WaitForSeconds(1);

        }
        ReadyForNewAi = true;

    }

    IEnumerator Sleeping() {

        while (tiredness>10)
        {
            tiredness -= 10;
            yield return new WaitForSeconds(1);

        }
        ReadyForNewAi = true;

    }

    IEnumerator Wander()
    {
        GetComponent<NavMeshAgent>().SetDestination(GetComponent<CowContext>().GetRandomDestination());
        while (Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination)>1.75f)
        {
         //   Debug.Log("Distance: " + Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination));
            
            yield return new WaitForSeconds(2);
        }
        ReadyForNewAi = true;


    }

    public void DestroyScr() {
        Destroy(this);

    }

    IEnumerator reproduce()
    {
        yield return new WaitForSeconds(10);
        Instantiate(Cow, transform.position + new Vector3(3, 0, 3),Quaternion.identity);
        ReadyForNewAi = true;
        GetComponent<CowAI>().mate = 0;

    }

    void StartWandering() {
     //   Debug.Log("Started to wander");
        ReadyForNewAi = false;
        gameObject.SendMessage("ScenForPositions");
        StartCoroutine("Wander");
        


        //Scan for new position
        //Choose Position and set as path


    }


    void StartGrazing()
    {
        ReadyForNewAi = false;
        StartCoroutine("Grazing");


    }
    void StartSleeping()
    {
        ReadyForNewAi = false;
        StartCoroutine("Sleeping");

    }
    void StartMating() {
        ReadyForNewAi = false;
        StartCoroutine("reproduce");



    }
    // Use this for initialization
    void Start () {
        hunger = 0;
        tiredness = 0;
        mate = 0;
        threshhold = 60;
        ReadyForNewAi = true;
        StartCoroutine("AIStart");
        StartCoroutine("Needs");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
