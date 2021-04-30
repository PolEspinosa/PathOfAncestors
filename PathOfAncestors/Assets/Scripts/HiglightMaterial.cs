using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiglightMaterial : MonoBehaviour
{
    private Material mat;
    private Camera mainCamera;
    private float minFresnel;
    private int maxFresnel;
    private Vector3 screenPoint;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mat = gameObject.GetComponent<Renderer>().material;
        minFresnel = mat.GetFloat("FresnelIntensity");
        maxFresnel = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnScreen())
        {
            mat.SetFloat("FresnelIntensity", minFresnel);
        }
        else
        {
            mat.SetFloat("FresnelIntensity", maxFresnel);
        }
    }

    private bool IsOnScreen()
    {
        screenPoint = mainCamera.WorldToViewportPoint(gameObject.transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
