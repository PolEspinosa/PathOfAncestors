using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritAnimatorController : SpiritsAnimatorController
{
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("invoked", invoked);
        animator.SetBool("uninvoked", uninvoked);
    }

    private void FireInvoked()
    {
        invoked = true;
    }

    private void DestroyFire()
    {
        destroySpirit = true;
    }
}
