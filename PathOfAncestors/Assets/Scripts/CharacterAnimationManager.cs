using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    private CMF.AdvancedWalkerController inputManager;
    private GameObject player;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputManager = player.GetComponent<CMF.AdvancedWalkerController>();
        playerAnimator = player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetFloat("Speed", Mathf.Clamp01(inputManager.savedVelocity.magnitude));
    }

    void FixedUpdate()
    {
        
    }
}
