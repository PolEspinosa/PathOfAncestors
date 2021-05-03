using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoxParticles : MonoBehaviour
{
    [SerializeField]
    private GameObject rockParticles1, rockParticles2, rockParticles3, rockParticles4;
    [SerializeField]
    private GameObject dustParticles;
    private GameObject player;
    private SpiritsPassiveAbilities1 passiveScript;
    // Start is called before the first frame update
    void Start()
    {
        rockParticles1.SetActive(false);
        rockParticles2.SetActive(false);
        rockParticles3.SetActive(false);
        rockParticles4.SetActive(false);
        dustParticles.SetActive(false);
        player = GameObject.Find("Character");
        passiveScript = player.GetComponent<SpiritsPassiveAbilities1>();
    }

    // Update is called once per frame
    void Update()
    {
        //only activate the particles when the player is pushing the box
        if (passiveScript.pushing)
        {
            rockParticles1.SetActive(true);
            rockParticles2.SetActive(true);
            rockParticles3.SetActive(true);
            rockParticles4.SetActive(true);
            dustParticles.SetActive(true);
        }
        else
        {
            rockParticles1.SetActive(false);
            rockParticles2.SetActive(false);
            rockParticles3.SetActive(false);
            rockParticles4.SetActive(false);
            dustParticles.SetActive(false);
        }
    }
}
