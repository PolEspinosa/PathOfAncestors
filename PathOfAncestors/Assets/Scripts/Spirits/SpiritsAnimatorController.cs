﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritsAnimatorController : MonoBehaviour
{
    public bool invoked, uninvoked, destroySpirit;
    protected Animator animator;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        invoked = uninvoked = destroySpirit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}