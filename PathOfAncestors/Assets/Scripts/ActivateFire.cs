using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFire : MonoBehaviour
{
    private Activator activator;
    public GameObject fire;
    public GameObject fireLight;
    [SerializeField]
    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        activator = gameObject.GetComponent<Activator>();
        fire.SetActive(activated);
        fireLight.SetActive(activated);
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            if (activator._activated)
            {
                fire.SetActive(true);
                fireLight.SetActive(true);
            }
            else
            {
                fire.SetActive(false);
                fire.SetActive(false);
            }
        }
    }
}
