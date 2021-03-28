using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
  
    public GameObject fireSpiritRef;
    public GameObject earthSpiritRef;
    public GameObject windSpiritRef;

    public GameObject fireWindPosition;
    public GameObject earthPosition;

    public Activator activatorObject;

    public GameObject currentSpirit = null;

    bool fireSpiritActivated = false;
    bool earthpiritActivated = false;
    bool windSpiritActivated = false;

    public OrderSystem order;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InvokeSpirit(fireSpiritRef, fireWindPosition.transform);
            //play fire spirit invokation sound
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Invocaciones/invokeFireSpirit");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InvokeSpirit(earthSpiritRef, earthPosition.transform);
            //play earth spirit invokation sound
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Invocaciones/invokeEarthSpirit");
        }
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    InvokeSpirit(windSpiritRef, fireWindPosition.transform);
        //}
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log(currentSpirit.GetComponent<BaseSpirit>().GetSpiritType());
        }
       
    }


    void InvokeSpirit(GameObject _spirit, Transform _position)
    {
        if( currentSpirit==null)
        {
            currentSpirit=Instantiate(_spirit, _position.position, Quaternion.identity);    
        }

        else
        {

            if (_spirit.tag != currentSpirit.tag)
            {
                Desinvoke(currentSpirit);
                currentSpirit=Instantiate(_spirit, _position.position, Quaternion.identity);
            }
            else
            {
                Desinvoke(currentSpirit);
            }
        }
    }


    void Desinvoke(GameObject _currentSpirit)
    {
       
        if(activatorObject != null)
        {
           if(activatorObject.GetComponent<PressurePlateActivator>())
            {
                activatorObject.GetComponent<PressurePlateActivator>().colliders.Remove(currentSpirit.transform.gameObject);
            }
           else if (activatorObject.GetComponent<OvenActivator>())
            {
                activatorObject.GetComponent<OvenActivator>().DeactivateOven();

            }
            else
            {
                activatorObject._activated = false;
                activatorObject.OnDeactivate();
                
            }
            activatorObject = null;


        }
        if(order.activator!=null)
        {
            order.activator = null;
        }

        Destroy(_currentSpirit);
        currentSpirit = null;
        order.isGoingToEarth = false;
    }
    

}
