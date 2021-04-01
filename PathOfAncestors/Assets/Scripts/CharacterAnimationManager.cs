using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    public CMF.AdvancedWalkerController inputManager;
    public CMF.CharacterInput charInput;
    public GameObject player;
    public Animator playerAnimator;
    private SpiritsPassiveAbilities spiritsPassiveAbilities;
    private bool canPlayJumpSound; //bool to control whether to play the jump sound or not
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = player.GetComponentInChildren<Animator>();
        spiritsPassiveAbilities = player.GetComponent<SpiritsPassiveAbilities>();
        canPlayJumpSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetFloat("Speed", Mathf.Clamp01(inputManager.savedVelocity.magnitude));
        if (!spiritsPassiveAbilities.pushing)
        {

        }
        playerAnimator.SetBool("isJumping", inputManager.GetJumpState());
        playerAnimator.SetBool("isGrounded", inputManager.GetGroundedState());
        playerAnimator.SetBool("noInput", !DetectMovementInput());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if play jump sound is enabled
            if (canPlayJumpSound)
            {
                //play jump sound
                FMODUnity.RuntimeManager.PlayOneShot("event:/Voice/jump");
                //disable play jump sound so it doesn't play more than once in one jump
                canPlayJumpSound = false;
            }
        }
        //if the player is grounded, enable play jump sound
        else if (inputManager.GetGroundedState())
        {
            canPlayJumpSound = true;
        }
    }

    void FixedUpdate()
    {
        
    }

    private bool DetectMovementInput()
    {
        return (charInput.GetHorizontalMovementInput() != 0 || charInput.GetVerticalMovementInput() != 0);
    }
}
