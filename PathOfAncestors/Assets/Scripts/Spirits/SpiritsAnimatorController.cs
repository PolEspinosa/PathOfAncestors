using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritsAnimatorController : MonoBehaviour
{
    public bool invoked, uninvoked, destroySpirit;
    protected Animator animator;
    public float speed;
    public bool hasToBreak;
    public bool going;
    public bool moveAfterGetUp;

    protected enum State
    {
        FOLLOWING, GOING
    }

    protected State earthSpiritState;

    public string stateString;

    // Start is called before the first frame update
    void Start()
    {
        invoked = uninvoked = destroySpirit = false;
        hasToBreak = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
