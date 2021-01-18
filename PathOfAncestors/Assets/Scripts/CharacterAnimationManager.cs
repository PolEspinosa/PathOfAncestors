using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    public CMF.AdvancedWalkerController inputManager;
    public CMF.CharacterInput charInput;
    public GameObject player;
    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
       
        playerAnimator = player.GetComponentInChildren<Animator>();
       
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
