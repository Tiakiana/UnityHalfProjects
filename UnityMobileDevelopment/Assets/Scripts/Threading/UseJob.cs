using UnityEngine;
using System.Collections;

public class UseJob : MonoBehaviour {
 public Job myJob;
    void Start()
    {
        myJob = new Job();
        myJob.InData = new Vector3[10];

        myJob.Start(); // Don't touch any data in the job class after you called Start until IsDone is true.
    }
    void Update()
    {
        if (myJob != null)
        {
            if (myJob.IsDone)
            {
                Debug.Log("Job's Done");
            }

            if (myJob.Update())
            {
                // Alternative to the OnFinished callback
                myJob = null;
            }
        }
    }
}
