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



    public GameObject currentSpirit = null;

    bool fireSpiritActivated = false;
    bool earthpiritActivated = false;
    bool windSpiritActivated = false;


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
        Destroy(_currentSpirit);
        currentSpirit = null;
    }
    

}
