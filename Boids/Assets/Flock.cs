using UnityEngine;
using System.Collections;

public class Flock : MonoBehaviour
{
    public float speed = 0.5f;
    public float rotationSpeed = 4;
    Vector3 averageHeading;
    Vector3 averagePosition;
    public float NeighborDistance = 2;
    public bool turn;

    // Use this for initialization
    void Start()
    {
        speed = Random.Range(0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position,Vector3.zero)>= globalFlock.tankSize)
        {
            turn = true;
        }
        else
        {
            turn = false;
        }
        if (turn)
        {
            Vector3 dir = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(dir),rotationSpeed * Time.deltaTime );
        }

        if (Random.Range(0, 5) < 1)
        {
            ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
        if (speed>15)
        {
            speed = 1;
        }
    }


    void ApplyRules()
    {
        GameObject[] gos;
        gos = globalFlock.allFish;
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;
        Vector3 goalPos = globalFlock.goalPos;
        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {


                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= NeighborDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;
                    if (dist < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);

                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                    //gSpeed =  anotherFlock.speed;

                }
            }
        }
        if (groupSize>0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }
}
