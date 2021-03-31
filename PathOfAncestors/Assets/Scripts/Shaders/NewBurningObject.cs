using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class NewBurningObject : MonoBehaviour
{
    private Material mat;
    private float burntAmount;
    public bool burning;
    public ParticleSystem fireParticles;
    private ParticleSystem.MainModule particlesMainModule;
    [SerializeField]
    private float burntThreshold;
    [SerializeField]
    private float burningSpeed;
    [SerializeField]
    private Collider col;
    // Start is called before the first frame update
    void Start()
    {
        burntAmount = 0;
        burning = false;
        mat = gameObject.GetComponent<MeshRenderer>().material;
        //particlesMainModule = fireParticles.main;
        fireParticles.Stop();
}

    // Update is called once per frame
    void Update()
    {
        //change material alpha amount
        mat.SetFloat("Vector1_54467BBE", burntAmount);
        if (burntAmount >= burntThreshold)
        {
            col.enabled = false;
        }
        if (burntAmount >= burntThreshold / 1.5f)
        {
            fireParticles.Stop();
        }
        if (burntAmount >= 1)
        {
            Destroy(gameObject);
        }
        
    }

    void FixedUpdate()
    {
        if (mat.GetFloat("Vector1_54467BBE") < 1 && burning)
            burntAmount += burningSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FIRE"))
        {
            burning = true;
            //other.gameObject.GetComponent<BaseSpirit>().MoveTo(other.gameObject.transform.position);
            fireParticles.Play();
        }
    }
}
