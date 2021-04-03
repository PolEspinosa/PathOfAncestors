using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpiritAnimatorController : SpiritsAnimatorController
{
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", speed);
    }
}
