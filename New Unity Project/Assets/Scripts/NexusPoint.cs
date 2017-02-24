using UnityEngine;
using System.Collections;

public class NexusPoint : MonoBehaviour {
    [TextArea(3, 10)]
    public string NexusText;
    [TextArea(3, 10)]
    public string ChoiceAText;
    [TextArea(3, 10)]
    public string ChoiceBText;
    [TextArea(3, 10)]
    public string ChoiceCText;
    
    public int[] Chances = new int[3];
    [TextArea(3, 10)]
    public string ChoiceASuccess;
    [TextArea(3, 10)]
    public string ChoiceAFailure;
    [TextArea(3, 10)]
    public string ChoiceBSuccess;
    [TextArea(3, 10)]
    public string ChoiceBFailure;
    [TextArea(3, 10)]
    public string ChoiceCSuccess;
    [TextArea(3, 10)]
    public string ChoiceCFailure;

    public GameObject NextChoice;

    void Start () {
        for (int i = 0; i < Chances.Length; i++)
        {
            Chances[i] = Random.Range(1, 101);
        }
            
        
	}
	
	void Update () {
	
	}
}
