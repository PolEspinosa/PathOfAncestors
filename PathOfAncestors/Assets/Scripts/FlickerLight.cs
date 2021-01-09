using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public float maxIntensity, minIntensity, flickerTime, flickerSpeed;
    private float currentTime, randIntensity;
    private Light fireLight;
    // Start is called before the first frame update
    void Start()
    {
        fireLight = gameObject.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentTime >= flickerTime)
        //{
        //    fireLight.intensity = Random.Range(minIntensity, maxIntensity);
        //    currentTime = 0;
        //}
        //else
        //{
        //    currentTime += Time.deltaTime;
        //}
        Flickering();
    }

    private void Flickering()
    {
        if (currentTime >= flickerTime)
        {
            randIntensity = Random.Range(minIntensity, maxIntensity);
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
        if (fireLight.intensity < randIntensity)
        {
            fireLight.intensity += Time.deltaTime * flickerSpeed;
        }
        else if (fireLight.intensity > randIntensity)
        {
            fireLight.intensity -= Time.deltaTime * flickerSpeed;
        }
        
    }
}
