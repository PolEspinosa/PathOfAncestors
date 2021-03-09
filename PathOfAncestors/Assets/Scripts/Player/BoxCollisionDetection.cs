using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisionDetection : MonoBehaviour
{
    public bool colliding;
    // Start is called before the first frame update
    void Start()
    {
        colliding = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            colliding = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            colliding = false;
        }
    }
}
