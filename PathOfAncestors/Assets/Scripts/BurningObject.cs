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

    // Start is called before the first frame update
    void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
        burntAmount = 0;
        burning = false;
        fireParticles.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (burning)
        {
            burntAmount += Time.deltaTime * burningSpeed;
        }
        mat.SetFloat("Vector1_2544001D", burntAmount);
        //when the object is invisible, destroy it
        if (burntAmount >= 1)
        {
            Destroy(gameObject);
        }
        //if there is almost no object left, stop particles
        else if(burntAmount >= 0.5f)
        {
            fireParticles.SetFloat("rate", 0);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FIRE"))
        {
            burning = true;
            other.gameObject.GetComponent<BaseSpirit>().MoveTo(other.gameObject.transform.position);
            fireParticles.enabled = true;
        }
    }
}
