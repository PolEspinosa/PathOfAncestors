using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    private CMF.AdvancedWalkerController inputManager;
    private CMF.CharacterInput charInput;
    private GameObject player;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputManager = player.GetComponent<CMF.AdvancedWalkerController>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        charInput = player.GetComponent<CMF.CharacterInput>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetFloat("Speed", Mathf.Clamp01(inputManager.savedVelocity.magnitude));
        playerAnimator.SetBool("isJumping", Input.GetKeyDown(KeyCode.Space));
        playerAnimator.SetBool("isGrounded", inputManager.GetGroundedState());
        playerAnimator.SetBool("noInput", !DetectMovementInput());
    }

    void FixedUpdate()
    {
        
    }

    private bool DetectMovementInput()
    {
        return (charInput.GetHorizontalMovementInput() != 0 || charInput.GetVerticalMovementInput() != 0);
    }
}
