using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public Checkpoint actualCheckpoint;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCheckpoint(Checkpoint checkpoint)
    {
        actualCheckpoint=checkpoint;
        SaveSystem.SavePlayerData(checkpoint);
    }

    public void moveToCheckpoint()
    {
        player.transform.position = actualCheckpoint.transform.position;
    }
}
