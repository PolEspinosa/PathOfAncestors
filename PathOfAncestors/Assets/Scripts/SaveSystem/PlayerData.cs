using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerPosition;
    public GameObject[] decoration;
    public bool[] decorationBools;


    public PlayerData(Checkpoint checkpoint, GameManager gameManager)
    {
        playerPosition = new float[3];

        playerPosition[0] = checkpoint.transform.position.x;
        playerPosition[1] = checkpoint.transform.position.y;
        playerPosition[2] = checkpoint.transform.position.z;

        decoration = new GameObject[gameManager.decorations.Length];
        decoration = gameManager.decorations;
        decorationBools = new bool[decoration.Length];
        for(int i = 0; i < decoration.Length; i++)
        {
            if (decoration[i].activeInHierarchy)
            {
                decorationBools[i] = true;
            }
            else
            {
                decorationBools[i] = false;
            }
        }
    }
}
