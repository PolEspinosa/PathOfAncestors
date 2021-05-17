using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacks : MonoBehaviour
{
    public GameObject player;
    public SpiritManager manager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");
        manager = player.GetComponent<SpiritManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(!manager.hasFire)
            {
                manager.hasFire = true;
            }
            if(!manager.hasEarth)
            {
                manager.hasEarth = true;
            }
        }
            
    }
}
