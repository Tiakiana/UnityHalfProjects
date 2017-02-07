using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CowContext : MonoBehaviour {

   public List<Vector3> viableposition = new List<Vector3>();
    public float samplingRange = 10;
    public float samplingDensity = 3F;

    public Vector3 GetRandomDestination() {
        return viableposition[Random.Range(0, viableposition.Count)];

    }
    public void DestroyScr()
    {
        Destroy(this);

    }

    public void ScenForPositions()
    {
        
       

        viableposition.Clear();

        var halfSamplingRange = this.samplingRange * 0.5f;
        var pos = transform.position;

        // nested loop in x and z directions, starting at negative half sampling range and ending at positive half sampling range, thus sampling in a square around the entity
        for (var x = -halfSamplingRange; x < halfSamplingRange; x += this.samplingDensity)
        {
            for (var z = -halfSamplingRange; z < halfSamplingRange; z += this.samplingDensity)
            {
                var p = new Vector3(pos.x + x, 0f, pos.z + z);
                /*
                NavMesh.SamplePosition(vec, out hit, dist, 1 << NavMesh.GetNavMeshLayerFromName("Default"));
    */
                NavMeshHit me;
                // NavMesh.SamplePosition(transform.position, out myNavHit, 100 , -1)
                if (NavMesh.SamplePosition(p, out me, 10, 1))
                    viableposition.Add(me.position);


                //Debug.Log(me);
                // Sample the position in the navigation mesh to ensure that the desired position is actually walkable
                NavMeshHit hit;
                if (NavMesh.SamplePosition(p, out hit, samplingDensity * 0.5f,1))
                {
                    viableposition.Add(hit.position);
                }
            }
        }
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (viableposition.Count>0)
        {
            foreach (Vector3 item in viableposition)
            {
            //    Debug.DrawRay(item, Vector3.up, Color.black);
            }
        }
        
	}
}
