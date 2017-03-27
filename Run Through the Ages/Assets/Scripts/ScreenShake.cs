using UnityEngine;
using System.Collections;
public class ScreenShake : MonoBehaviour
{
    public Vector3 originalpositionsetter;
    private Vector3 originPosition;
    private Quaternion originRotation;
    private Quaternion originRotationsetter;
  //  public GameObject origpos;
    public float shakyness;
    public float decayness;
    float shake_decay = .3f;
    float shake_intensity = 0.002F;
    public float timestop = 0.02f;


   
    void Awake()
    {
        originalpositionsetter = transform.position;
        originRotationsetter = transform.rotation;

    }
    void Start()
    {

        originalpositionsetter = transform.position;
    }

    //Sætter en pause så man lægger mere mærke til screenshaket uden det bliver længere.
    public void MiniPause() {

        StartCoroutine("pauser",0.05f);
    }



    //En genial metode, der kan måle den tid, som er gået selvom spillet er sat på pause.
    public static class CoroutineUtil
 {
     public static IEnumerator WaitForRealSeconds(float time)
     {
         float start = Time.realtimeSinceStartup;
         while (Time.realtimeSinceStartup < start + time)
         {
             yield return null;
         }
     }
 }
    

    IEnumerator pauser(float time) {
        float start = Time.realtimeSinceStartup;


        Time.timeScale = 0;

      
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
        Time.timeScale = 1;

    }

    IEnumerator shaker()
    {
        while (shake_intensity > 0)
        {


            if (shake_intensity > 0)
            {
                transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
                transform.rotation = new Quaternion(
                    originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
                    originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
                    originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
                    originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
                shake_intensity -= shake_decay;
            }
            else if (transform.position != originalpositionsetter)
            {
                transform.position = originalpositionsetter;

            }
            yield return null;
        }
        transform.position = originalpositionsetter;
        transform.rotation = originRotationsetter;
    }

    void Update()
    {

       

    }

    public void SetPosition()
    {
        originalpositionsetter = transform.position;
    }

    public void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = shakyness;
        shake_decay = decayness;
        StartCoroutine("shaker");

    }
}