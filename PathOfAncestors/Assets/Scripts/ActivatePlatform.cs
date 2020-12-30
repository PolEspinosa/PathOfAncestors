using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    public bool active; //whether the platform has been activated
    private Vector3 startPos, endPos;
    public float yOffset, movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //get the starting position of the platform
        startPos = gameObject.transform.position;
        //get the desired position of the platform once activated
        endPos = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        //if active and the current position of the platform is not far enough from the start position, keep increasing y position
        if (active && Vector3.Distance(gameObject.transform.position, startPos) < yOffset)
        {
            gameObject.transform.position += new Vector3(0, movementSpeed, 0) * Time.deltaTime;
        }
        //if not active and the current position of the platform is not far enough from the end position, keep decreasing y position
        else if (!active && Vector3.Distance(gameObject.transform.position, endPos) < yOffset)
        {
            gameObject.transform.position -= new Vector3(0, movementSpeed, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
