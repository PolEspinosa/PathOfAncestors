using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class NewBurningObject : MonoBehaviour
{
    private Material mat;
    private float burntAmount;
    public bool burning;
    public VisualEffect fireParticles;
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
        fireParticles.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //change material alpha amount
        mat.SetFloat("Vector1_54467BBE", burntAmount);
        if (burntAmount >= burntThreshold)
        {
            fireParticles.SetFloat("rate", 0);
            col.enabled = false;
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
            fireParticles.enabled = true;
        }
    }
}
