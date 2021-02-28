using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BurningObject : MonoBehaviour
{
    private Material mat;
    private float burntAmount;
    public bool burning;
    public float burningSpeed;
    public VisualEffect fireParticles;

    public float noiseStrength;
    public float objectHeight;
    private float time, height, startHeight, startTime;

    //at which difference of heigths the object should destroy and remove colliders
    public float destructionLimit, noCollisionLimit;

    // Start is called before the first frame update
    void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
        burntAmount = 0;
        burning = false;
        fireParticles.enabled = false;
        //the sine mas value is 1, so the in order for the gameobject to start without any amount of dissolve
        //it is necessary for the time to start at 1
        time = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if (burning)
        //{
        //    burntAmount += Time.deltaTime * burningSpeed;
        //}
        //mat.SetFloat("Vector1_2544001D", burntAmount);
        ////when the object is invisible, destroy it
        //if (burntAmount >= 1)
        //{
        //    Destroy(gameObject);
        //}
        ////if there is almost no object left, stop particles
        //else if(burntAmount >= 0.5f)
        //{
        //    fireParticles.SetFloat("rate", 0);
        //    gameObject.GetComponent<Collider>().enabled = false;
        //}
        if (burning)
        {
            time -= Time.deltaTime * Mathf.PI * burningSpeed;
            height = gameObject.transform.position.x;
            height += Mathf.Sin(time) * (objectHeight / 2.0f);
            SetHeight(height);
        }
        //when the object is invisible, destroy it
        if (Mathf.Abs(startHeight-height) >= objectHeight / destructionLimit)
        {
            Destroy(gameObject);
        }
        //if there is almost no object left, stop particles
        else if (Mathf.Abs(startHeight - height) >= objectHeight / noCollisionLimit)
        {
            fireParticles.SetFloat("rate", 0);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void SetHeight(float _height)
    {
        mat.SetFloat("Vector1_CF698F65", _height);
        mat.SetFloat("Vector1_4D027A4B", noiseStrength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FIRE"))
        {
            //get starting height
            //startTime = Time.time * Mathf.PI * burningSpeed;
            startHeight = gameObject.transform.position.x + time;
            //startHeight += Mathf.Sin(startTime) * (objectHeight / 2.0f);
            
            burning = true;
            other.gameObject.GetComponent<BaseSpirit>().MoveTo(other.gameObject.transform.position);
            fireParticles.enabled = true;
        }
    }
}
