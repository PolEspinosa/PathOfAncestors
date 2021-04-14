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

    private FireSpiritAnimatorController fireController;
    public float timesInvoked;

    //this variable will determine when to spawn the other spirit so it goes according to the animation
    private bool invokeOtherSpirit;
    private GameObject currentSpiritAux;
    private Vector3 positionAux;

    // Start is called before the first frame update
    void Start()
    {
        invokeOtherSpirit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InvokeSpirit(fireSpiritRef, fireWindPosition.transform);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InvokeSpirit(earthSpiritRef, earthPosition.transform);
        }
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    InvokeSpirit(windSpiritRef, fireWindPosition.transform);
        //}
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log(currentSpirit.GetComponent<BaseSpirit>().GetSpiritType());
        }

        //if has to destroy the spirit
        if (currentSpirit != null && currentSpirit.CompareTag("FIRE"))
        {
            if (currentSpirit.GetComponentInChildren<SpiritsAnimatorController>().destroySpirit)
            {
                if (invokeOtherSpirit)
                {
                    Desinvoke(currentSpirit);
                    currentSpirit = Instantiate(currentSpiritAux, positionAux, Quaternion.identity);
                    invokeOtherSpirit = false;
                }
                else
                {
                    Desinvoke(currentSpirit);
                }
            }
        }
    }


    void InvokeSpirit(GameObject _spirit, Transform _position)
    {
        if( currentSpirit==null)
        {
            currentSpirit =Instantiate(_spirit, _position.position, Quaternion.identity);    
        }

        else
        {
            if (_spirit.tag != currentSpirit.tag)
            {
                //place holder until we have earth spirit animations
                if (currentSpirit.CompareTag("FIRE"))
                {
                    currentSpiritAux = _spirit;
                    positionAux = _position.position;
                    invokeOtherSpirit = true;
                    currentSpirit.GetComponentInChildren<SpiritsAnimatorController>().uninvoked = true;
                }
                else
                {
                    Desinvoke(currentSpirit);
                    currentSpirit=Instantiate(_spirit, _position.position, Quaternion.identity);
                }
            }
            else
            {
                //place holder until we have earth spirit animations
                if (currentSpirit.CompareTag("FIRE"))
                {
                    currentSpirit.GetComponentInChildren<SpiritsAnimatorController>().uninvoked = true;
                }
                else
                {
                    Desinvoke(currentSpirit);
                }
            }
        }
    }


    void Desinvoke(GameObject _currentSpirit)
    {
        if (activatorObject != null)
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
