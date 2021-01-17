using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenActivator : Activator
{
    public Material activeMaterial;
    public Material defaultMaterial;
    public Transform endPos;
    public GameObject ovenParticles;

    GameObject fireSpirit;


    private void Update()
    {
        //if(!_activated)
        //{
        //    this.gameObject.transform.parent.GetComponent<MeshRenderer>().material = defaultMaterial;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_activated)
        {
            if (other.tag == "FIRE" && order.activator == this.gameObject)
            {
               fireSpirit = other.gameObject;
               StartCoroutine( activeOven(2f , fireSpirit));
                ovenParticles.SetActive(true);
            }

        }
    }

   
    public void DeactivateOven()
    {
        ovenParticles.SetActive(false);
        fireSpirit.GetComponent<BaseSpirit>().MoveTo(endPos.position);
        StartCoroutine(shutDownOven(2f));
    }

    IEnumerator activeOven(float waitTime, GameObject fireSpirit)
    {
        yield return new WaitForSeconds(waitTime);

        _activated = true;
        OnActivate();
        manager.activatorObject = this;
        //this.gameObject.transform.parent.GetComponent<MeshRenderer>().material = activeMaterial;
    }

    IEnumerator shutDownOven(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _activated = false;
        OnDeactivate();
        manager.activatorObject = null;
        fireSpirit = null;

    }
}
