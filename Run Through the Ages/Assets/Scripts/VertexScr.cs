using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class VertexScr : MonoBehaviour
{
    private MeshFilter sMeshFilter;
    public GameObject farmand;
    public List<GameObject> steps = new List<GameObject>();
	// Use this for initialization
	void Start ()
	{
        var thisMatrix = transform.localToWorldMatrix;
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;

	    
        
	    int h = 0;
	    for (int i = 2438; i < 2496; i++)
	    {
            //  print("mesh1 vertex at " + thisMatrix.MultiplyPoint3x4(vertices[i]));

           // GameObject pawn = GameObject.CreatePrimitive(PrimitiveType.Cube);
           GameObject pawn = new GameObject();
	        pawn.transform.position = thisMatrix.MultiplyPoint3x4(vertices[i]);
	        pawn.gameObject.name = ""+h;
            pawn.transform.SetParent(farmand.transform);
	        h++;
            steps.Add(pawn);
	    }

	    for (int i = 57; i > 6; i--)
	    {
	        EncounterController.EncCtrl.StepPositions.Add(steps[i]);
	    }

        //-----------------------------

        /*
                sMeshFilter = GetComponent<MeshFilter>();
                Mesh mesh = GetComponent<MeshFilter>().mesh;
                Vector3[] vertices = mesh.vertices;

                Debug.Log(mesh.vertices[6]);
                for (int i = 2400; i <= 2498; i++)
                {
                    Instantiate(pawn, vertices[i], Quaternion.identity);
                }
                */
    }

    // Update is called once per frame
    void Update()
    {
      /*  Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int i = 2498;
        int x = 2497;
        int f = 2496;

       // vertices[i] += Vector3.left * Time.deltaTime;
        //vertices[x] += -transform.up * Time.deltaTime;
        vertices[f] += -transform.up * Time.deltaTime;

        Debug.Log(vertices[x]);
        Debug.Log(vertices[f] +"F is");
        Debug.Log(vertices[i]);

        //vertices[f] += Vector3.left * Time.deltaTime;
        //  Debug.Log(mesh.vertices.Length);
        
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    */
    }
    
}
