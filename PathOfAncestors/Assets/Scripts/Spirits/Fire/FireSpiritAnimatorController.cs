using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritAnimatorController : SpiritsAnimatorController
{
    [SerializeField]
    private ParticleSystem fireParticles;
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
        animator.SetFloat("Speed", speed);
    }

    private void FireInvoked()
    {
        invoked = true;
        //fireParticles.Play();
    }

    private void DestroyFire()
    {
        destroySpirit = true;
    }

    private void StopParticles()
    {
        fireParticles.Stop();
    }
}
