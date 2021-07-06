using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeightDoorActivable : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    Vector3 endPosAux;
    public Vector3 targetPos;
    public GameObject activator;
    public float speed;

    

    // Start is called before the first frame update
    void Start()
    {
        startPos =transform.position ;
        endPosAux = endPos;
        endPos = new Vector3(startPos.x,endPos.y+startPos.y,startPos.z);

    }

    // Update is called once per frame
    void Update()
    {
        
        if(activator.GetComponent<WeightPlateActivator>().weight<100)
        {
            if(activator.GetComponent<WeightPlateActivator>().weight >0)
            {
                speed = activator.GetComponent<WeightPlateActivator>().weight/10;
                targetPos = new Vector3(endPos.x, (endPosAux.y * activator.GetComponent<WeightPlateActivator>().weight/100)+startPos.y, endPos.z);
            }
            else if (activator.GetComponent<WeightPlateActivator>().weight <= 0 && targetPos!=startPos)
            {
                targetPos = startPos;
                speed = 15;
            }


        }
        
        else if(activator.GetComponent<WeightPlateActivator>().weight == 100)
        {
            targetPos = endPos;
        }
        

        
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

      
    }

  
}
