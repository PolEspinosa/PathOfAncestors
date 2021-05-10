using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritAnimatorControllerTut : SpiritsAnimatorController
{
    [SerializeField]
    private ParticleSystem fireParticles;
    [SerializeField]
    private float minIdleChangeTime, maxIdleChangeTime;
    private float currentTime, changeTime;
    private bool changeIdle;
    private bool doOnce;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        changeIdle = false;
        currentTime = Random.Range(minIdleChangeTime, maxIdleChangeTime);
        doOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("invoked", invoked);
        animator.SetBool("uninvoked", uninvoked);
        animator.SetFloat("Speed", speed);
        animator.SetBool("changeIdle", changeIdle);
    }

    private void FixedUpdate()
    {
        ChangeIdle();
    }

    private void FireInvoked()
    {
        invoked = true;
        //fireParticles.Play();
    }

    private void DestroyFire()
    {
        destroySpirit = true;
        NoInputFire.noInput = false;
    }

    private void StopParticles()
    {
        fireParticles.Stop();
    }

    private void ResetIdleTimer()
    {
        changeIdle = false;
        currentTime = Random.Range(minIdleChangeTime, maxIdleChangeTime);
    }

    private void ChangeIdle()
    {
        if (speed <= 0.2f)
        {
            doOnce = true;
            //if it doesn't have to change idle state yet, decrease counter, else change state
            if (!changeIdle)
            {
                if (currentTime > 0)
                {
                    currentTime -= Time.fixedDeltaTime;
                }
                else
                {
                    changeIdle = true;
                }
            }
        }
        //if the speed is greater than 0.1f means it is moving, then reset timer
        else
        {
            if (doOnce)
            {
                currentTime = Random.Range(minIdleChangeTime, maxIdleChangeTime);
                doOnce = false;
            }
        }
    }
}
