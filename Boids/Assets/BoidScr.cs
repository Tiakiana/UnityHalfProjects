using UnityEngine;
using System.Collections;
using System.Threading;
using JetBrains.Annotations;

public class BoidScr : MonoBehaviour
{

    public GameObject Neighbour;
    public float Alignment, Cohesion, Separation, Perception,PersonalSpace;
    public float Volatility = 100;


	void Start ()
	{
	    StartCoroutine("Move");
        StartCoroutine("SetDirection");
	    //  StartCoroutine("Eyes");
	}

    IEnumerator Move()
    {
        while (true)
        {

            transform.position += transform.up * 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Turn()
    {
        while (true)
        {
           // Neighbour = Physics.OverlapSphere(transform.position,10f).gameObject;

          //  Direction = transform.up;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Eyes()
    {
        while (true)
        {
            
        Collider[]  cols = Physics.OverlapSphere(transform.position, 6);
            
            
            yield return new WaitForSeconds(0.01f);

        }
    }


    IEnumerator SetDirection()
    {
        while (true)
        {
            EyesOn();
            
            if (Neighbour!= null)
            {
                //Vector3 vec = Quaternion.Euler(transform.rotation);

                transform.eulerAngles += CalcCost() / Volatility * Neighbour.transform.up;

            }
            yield return  new WaitForSeconds(0.01f);
        }
    }

    public void EyesOn()
    {
        Neighbour = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, Perception);
        Collider Nearest = null;

        float distance = 9999;

        foreach (Collider col in cols)
        {
            if (col.gameObject.transform.root != transform.root)
            {
                float distcur = Vector2.Distance(transform.position, col.gameObject.transform.position);
                if (distcur< distance)
                {
                    distance = distcur;
                    Nearest = col;
                }
            }
        }

        if (Nearest!= null)
        {
            Neighbour = Nearest.gameObject;
        }

    }

    // 
    //Cohesion: a positive score for getting closer to the average position of the neighbors
    //Separation: a negative score for getting too close to any one neighbor
    //Alignment: a positive score for getting closer to the average heading of the neighbors

    public float CalcCost()
    {
        float res = 0;
        if (Neighbour!= null)
        {

            res = CalcCohesionAndSeparation(Neighbour) + CalcAlignment(Neighbour);

        }
        return res;
    }

    public float CalcCohesionAndSeparation(GameObject neig)
    {
        float res = 100;
        float N = Vector2.Distance(transform.position, neig.transform.position);
        if (N-PersonalSpace>0)
        {
            res= 100/(N - PersonalSpace) * Cohesion;
        }
        
        if (N - PersonalSpace < 0)
        {
            res = 100 / (N - PersonalSpace) * Separation;
        }
        return res;
    }

    public float CalcAlignment(GameObject neig)
    {
        float res = 0;

        Vector3 vect = transform.up - neig.transform.up;

        res = Mathf.Abs(vect.x) + Mathf.Abs(vect.y) + Mathf.Abs(vect.z);

        res /= 100;
        


        return res * Alignment;
    }


    void Update () {
	
	}
}
