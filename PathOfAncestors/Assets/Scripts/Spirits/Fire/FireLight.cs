using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    private Material spiritMat;
    private Light fireLight; //the light the fire has
    private float currentIntensity; //the intensity the light has at the moment
    public float maxIntensity, minIntensity; //the max and minimum intensity the light can reach
    private bool increase; //determines whether the intensity goes up or down
    private float currentGlow; //the glow the spirit has at the moment
    public float maxGlow, minGlow; //the max and min glow the spirit can reach
    // Start is called before the first frame update
    void Start()
    {
        spiritMat = gameObject.GetComponent<MeshRenderer>().material;
        fireLight = gameObject.GetComponentInChildren<Light>();
        currentIntensity = maxIntensity;
        fireLight.intensity = currentIntensity;
        increase = true;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeIntensity();
    }

    //this function will increase and decrease the current intensity between min and max values
    private void ChangeIntensity()
    {
        //once the intensity reaches the maximum, increase equals false
        if (currentIntensity >= maxIntensity)
        {
            increase = false;
        }
        //once the intensity reaches the minimun, increase equals true
        else if (currentIntensity <= minIntensity)
        {
            increase = true;
        }
        //while increase equals true, increase intensity
        if (increase)
        {
            currentIntensity += Time.deltaTime;
            currentGlow -= Time.deltaTime / 3;
        }
        //while increase equals false, decrease intensity
        else if (!increase)
        {
            currentIntensity -= Time.deltaTime;
            currentGlow += Time.deltaTime / 3;
        }
        fireLight.intensity = currentIntensity;
        spiritMat.SetFloat("Vector1_9CDF794", currentGlow);
    }
}
