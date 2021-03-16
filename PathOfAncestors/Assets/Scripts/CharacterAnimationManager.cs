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
    // Start is called before the first frame update
    void Start()
    {
       
        playerAnimator = player.GetComponentInChildren<Animator>();
        spiritsPassiveAbilities = player.GetComponent<SpiritsPassiveAbilities>();
       
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
    }

    void FixedUpdate()
    {
        
    }

    private bool DetectMovementInput()
    {
        return (charInput.GetHorizontalMovementInput() != 0 || charInput.GetVerticalMovementInput() != 0);
    }
}
