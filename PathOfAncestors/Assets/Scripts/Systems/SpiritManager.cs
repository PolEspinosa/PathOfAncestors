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

    public bool hasFire = false;
    public bool hasEarth = false;

    private FireSpiritAnimatorController fireController;

    //this variable will determine when to spawn the other spirit so it goes according to the animation
    private bool invokeOtherSpirit, canInvoke;
    private GameObject currentSpiritAux;
    private GameObject aux; //get the spirit new invoked spirit
    private Vector3 positionAux;

    //variables to switch the music depending on the invoked spirit
    public float fireInvoked; //0 == none, 1 == invoked
    public float earthInvoked; //0 == none, 1 == invoked

    [SerializeField]
    private LayerMask ignoreMask;

    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        invokeOtherSpirit = false;
        fireInvoked = earthInvoked = 0;
        canInvoke = true;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InvokeSpirit(fireSpiritRef, fireWindPosition.transform.position);
            //if invoking the fire spirit, stop music from earth spirit and start music from fire spirit
            if (fireInvoked == 0)
            {
                earthInvoked = 0;
                fireInvoked = 1;
            }
            else
            {
                fireInvoked = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RaycastHit hit;
            if(Physics.Raycast(mainCamera.transform.position, earthPosition.transform.position-mainCamera.transform.position,out hit, (earthPosition.transform.position - mainCamera.transform.position).magnitude, ~ignoreMask))
            {
                InvokeSpirit(earthSpiritRef, hit.point);
            }
            else
            {
                InvokeSpirit(earthSpiritRef, earthPosition.transform.position);
            }
            //if invoking the earth spirit, stop music from fire spirit and start music from earth spirit
            if (earthInvoked == 0)
            {
                fireInvoked = 0;
                earthInvoked = 1;
            }
            else
            {
                earthInvoked = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log(currentSpirit.GetComponent<BaseSpirit>().GetSpiritType());
        }

        if (currentSpirit != null)
        {
            if (invokeOtherSpirit && canInvoke)
            {
                //Desinvoke(currentSpirit);
                aux = Instantiate(currentSpiritAux, positionAux, Quaternion.identity);
                invokeOtherSpirit = false;
                canInvoke = false;
            }
            if (currentSpirit.GetComponentInChildren<SpiritsAnimatorController>().destroySpirit)
            {
                Desinvoke(currentSpirit);
                if (aux != null)
                {
                    currentSpirit = aux;
                    aux = null;
                }
            }
        }
    }


    public void InvokeSpirit(GameObject _spirit, Vector3 _position)
    {
        if( currentSpirit==null)
        {
            currentSpirit=Instantiate(_spirit, _position, Quaternion.identity);
        }

        else
        {
            if (_spirit.tag != currentSpirit.tag && canInvoke)
            {
                currentSpiritAux = _spirit;
                positionAux = _position;
                invokeOtherSpirit = true;
                currentSpirit.GetComponentInChildren<SpiritsAnimatorController>().uninvoked = true;
            }
            else
            {
                currentSpirit.GetComponentInChildren<SpiritsAnimatorController>().uninvoked = true;
            }
        }
    }


    public void Desinvoke(GameObject _currentSpirit)
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
        canInvoke = true;
    }
}
