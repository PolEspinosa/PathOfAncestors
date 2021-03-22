using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLight : MonoBehaviour
{
    public bool hasLight;
    public GameObject enterZone, exitZone;
    private Color ambienceLightColor;
    // Start is called before the first frame update
    void Start()
    {
        hasLight = true;
        ambienceLightColor = RenderSettings.ambientLight;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLight)
        {
            if(RenderSettings.ambientLight.r < ambienceLightColor.r)
            {
                RenderSettings.ambientLight += new Color(Time.deltaTime / 3.5f, Time.deltaTime / 3.5f, Time.deltaTime / 3.5f);
                RenderSettings.reflectionIntensity += Time.deltaTime / 2.5f;
            }
            else if(RenderSettings.ambientLight.r > ambienceLightColor.r)
            {
                RenderSettings.ambientLight = ambienceLightColor;
            }
            if (RenderSettings.reflectionIntensity > 1)
            {
                RenderSettings.reflectionIntensity = 1;
            }
        }
        else if (!hasLight)
        {
            if (RenderSettings.ambientLight.r > 0)
            {
                RenderSettings.ambientLight -= new Color(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);
                RenderSettings.reflectionIntensity -= Time.deltaTime * 2;
            }
        }
    }
}
