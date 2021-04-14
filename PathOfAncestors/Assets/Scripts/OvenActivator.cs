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
        if (!_activated)
        {
            ovenParticles.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_activated)
        {
            if (other.tag == "FIRE" && order.activator == this.gameObject)
            {
                DataManager.totalTimesActivated++;
               fireSpirit = other.gameObject;
               StartCoroutine( activeOven(2f , fireSpirit));
                
            }

        }
    }

   
    public void DeactivateOven()
    {
        
        fireSpirit.GetComponent<BaseSpirit>().MoveTo(endPos.position);
        
        StartCoroutine(shutDownOven(2f));
    }

    IEnumerator activeOven(float waitTime, GameObject fireSpirit)
    {
        yield return new WaitForSeconds(waitTime);
        ovenParticles.SetActive(true);
        _activated = true;
        OnActivate();
        manager.activatorObject = this;
        //this.gameObject.transform.parent.GetComponent<MeshRenderer>().material = activeMaterial;
        //start the sound of the oven when activated
       ovenSoundInstance.start();
    }

    IEnumerator shutDownOven(float waitTime)
    {
        manager.activatorObject = null;
        fireSpirit = null;
        yield return new WaitForSeconds(waitTime);
        ovenParticles.SetActive(false);
        _activated = false;
        OnDeactivate();
        //stop the sound of the oven when deactivated
        ovenSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
