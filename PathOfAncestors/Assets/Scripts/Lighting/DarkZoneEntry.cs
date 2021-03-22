using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkZoneEntry : MonoBehaviour
{
    public ControlLight controlLight;

    private GameObject player;
    private SpiritsPassiveAbilities passive;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        passive = player.GetComponent<SpiritsPassiveAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (passive.inDarkArea)
        {
            if (other.gameObject.CompareTag("PartDoor"))
            {
                controlLight.hasLight = false;
            }
        }
    }
}
