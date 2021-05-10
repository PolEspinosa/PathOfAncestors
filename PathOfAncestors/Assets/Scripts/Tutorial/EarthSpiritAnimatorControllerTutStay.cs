﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EarthSpiritAnimatorControllerTutStay : SpiritsAnimatorController
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

    private FMOD.Studio.EventInstance stepsInstance;

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

    private void StopInvokedParticles()
    {
        invokedParticles.Stop();
        invokedParticles2.Stop();
    }

    private void UninvokedParticles()
    {
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
    }

    private IEnumerator particlesDelay()
    {
        yield return new WaitForSeconds(0.6f);
        destroySpirit = true;
        NoInputEarth.noInput = false;
    }

    private void WalkStep()
    {
        if (stateString == "FOLLOWING")
        {
            stepsInstance = FMODUnity.RuntimeManager.CreateInstance("event:/EarthSteps/earthSpiritSteps");
            stepsInstance.setVolume(0.5f);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(stepsInstance, gameObject.transform, gameObject.GetComponentInParent<Rigidbody>());
            stepsInstance.start();
            stepsInstance.release();
        }
    }

    private void RunStep()
    {
        if (stateString == "GOING")
        {
            stepsInstance = FMODUnity.RuntimeManager.CreateInstance("event:/EarthSteps/earthSpiritSteps");
            stepsInstance.setVolume(0.5f);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(stepsInstance, gameObject.transform, gameObject.GetComponentInParent<Rigidbody>());
            stepsInstance.start();
            stepsInstance.release();
        }
    }

    private void OnDestroy()
    {
        stepsInstance.release();
    }
}
