using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInputFire : MonoBehaviour
{
    public static bool noInput;

    // Start is called before the first frame update
    void Start()
    {
        noInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            noInput = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
