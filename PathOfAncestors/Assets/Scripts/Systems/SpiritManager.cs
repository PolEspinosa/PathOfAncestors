using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
  
    public GameObject fireSpiritRef;
    public GameObject earthSpiritRef;
    public GameObject windSpiritRef;

    private GameObject pathFollower;
    public GameObject pathFollowerRef;

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
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InvokeSpirit(earthSpiritRef, earthPosition.transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InvokeSpirit(windSpiritRef, fireWindPosition.transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log(currentSpirit.GetComponent<BaseSpirit>().GetSpiritType());
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) Destroy(pathFollower);
    }


    void InvokeSpirit(GameObject _spirit, Transform _position)
    {
        if( currentSpirit==null)
        {
            //don't instantiate the pathfollower gameobject if the spirit is not the fire spirit
            if (_spirit.CompareTag("FIRE"))
            {
                pathFollower = Instantiate(pathFollowerRef, fireWindPosition.transform.position, Quaternion.identity);
            }
            
            currentSpirit =Instantiate(_spirit, _position.position, Quaternion.identity);    
        }

        else
        {

            if (_spirit.tag != currentSpirit.tag)
            {
                Desinvoke(currentSpirit);
                //don't instantiate the pathfollower gameobject if the spirit is not the fire spirit
                if (_spirit.CompareTag("FIRE"))
                {
                    pathFollower = Instantiate(pathFollowerRef, fireWindPosition.transform.position, Quaternion.identity);
                }
                currentSpirit =Instantiate(_spirit, _position.position, Quaternion.identity);
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

        if (pathFollower != null)
        {
            Destroy(pathFollower);
        }

        Destroy(_currentSpirit);
        currentSpirit = null;
        order.isGoingToEarth = false;
    }
    

}
