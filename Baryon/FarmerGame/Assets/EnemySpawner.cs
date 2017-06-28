using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject Enemy;
    public float Health, Speed;


	void Start () {
        StartCoroutine("Spawn");
        StartCoroutine("Difficulty");
	}



    IEnumerator Spawn() {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1,11));
          GameObject go =  Instantiate(Enemy, new Vector3(transform.position.x, transform.position.y + Random.Range(-10, 11), 0), Quaternion.identity) as GameObject;
            go.GetComponent<EnemyScr>().health = Health;
            go.GetComponent<EnemyScr>().Speed = Speed;
        }

    }
    IEnumerator Difficulty() {
        while (true)
        {
            yield return new WaitForSeconds(10);
            int i = Random.Range(1,3);
            if (i == 1)
            {
                Health += 1;
            }
            else
            {
                Speed += 0.003f;
            }

            if (i == 3) 
            {
                Debug.Log("Error with random.");
            }
        }

    }
	
	void Update () {
	
	}
}
