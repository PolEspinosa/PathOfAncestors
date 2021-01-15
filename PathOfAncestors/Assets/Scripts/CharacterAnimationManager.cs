using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    private CMF.CharacterInput inputManager;
    private GameObject player;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputManager = player.GetComponent<CMF.CharacterInput>();
        playerAnimator = player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerAnimator.GetBool("moving"));
        if(inputManager.GetHorizontalMovementInput()!=0 || inputManager.GetVerticalMovementInput() != 0)
        {
            playerAnimator.SetBool("moving", true);
        }
        else
        {
            playerAnimator.SetBool("moving", false);
        }
    }
}
