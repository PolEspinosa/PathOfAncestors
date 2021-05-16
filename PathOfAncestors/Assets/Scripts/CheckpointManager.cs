using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public Checkpoint actualCheckpoint;
    public GameObject player;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCheckpoint(Checkpoint checkpoint)
    {
        actualCheckpoint=checkpoint;
        SaveSystem.SavePlayerData(checkpoint, gameManager.GetComponent<GameManager>());
    }

    public void moveToCheckpoint()
    {
        player.transform.position = actualCheckpoint.transform.position;
    }
}
