using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    CheckpointManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            manager.moveToCheckpoint();
            //play death sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/death");
        }
    }
}
