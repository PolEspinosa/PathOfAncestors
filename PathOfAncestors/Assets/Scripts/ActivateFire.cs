using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFire : MonoBehaviour
{
    private Activator activator;
    public GameObject fire;
    public GameObject fireLight;
    // Start is called before the first frame update
    void Start()
    {
        activator = gameObject.GetComponent<Activator>();
        fire.SetActive(false);
        fireLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (activator._activated)
        {
            fire.SetActive(true);
            fireLight.SetActive(true);
        }
        else
        {
            fire.SetActive(false);
            fireLight.SetActive(false);
        }
    }
}
