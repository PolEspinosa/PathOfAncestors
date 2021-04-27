using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EarthSpiritAnimatorController : SpiritsAnimatorController
{
    [SerializeField]
    private VisualEffect invokedParticles;
    [SerializeField]
    private ParticleSystem invokedParticles2;
    [SerializeField]
    private GameObject uninvokedParticlesObject;
    [SerializeField]
    private VisualEffect uninvokedParticles;
    [SerializeField]
    private ParticleSystem uninvokedParticles2;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", speed);
        animator.SetBool("invoked", invoked);
        animator.SetBool("breakWall", hasToBreak);
        animator.SetBool("uninvoked", uninvoked);
    }

    private void EarthInvoked()
    {
        invoked = true;
    }

    private void InvokedParticles()
    {
        //invokedParticles.Play();
        //invokedParticles2.Play();
    }

    private void StopInvokedParticles()
    {
        invokedParticles.Stop();
        invokedParticles2.Stop();
    }

    private void UninvokedParticles()
    {
        //uninvokedParticles.Play();
        //uninvokedParticles2.Play();
        uninvokedParticlesObject.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Invocaciones/invokeEarthSpirit", gameObject);
    }

    private void StopUninvokedParticles()
    {
        uninvokedParticles.Stop();
        uninvokedParticles2.Stop();
    }

    private void DestroyEarth()
    {
        StartCoroutine(particlesDelay());
        //destroySpirit = true;
    }

    private IEnumerator particlesDelay()
    {
        yield return new WaitForSeconds(0.6f);
        destroySpirit = true;
    }
}
