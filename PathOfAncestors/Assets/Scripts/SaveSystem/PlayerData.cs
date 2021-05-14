using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerPosition;

    public PlayerData(Checkpoint checkpoint)
    {
        playerPosition = new float[3];

        playerPosition[0] = checkpoint.transform.position.x;
        playerPosition[1] = checkpoint.transform.position.y;
        playerPosition[2] = checkpoint.transform.position.z;
    }
}
