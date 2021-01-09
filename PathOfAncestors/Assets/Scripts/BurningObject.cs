using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningObject : MonoBehaviour
{
    private Material mat;
    private float burntAmount;
    public bool burning;
    public float burningSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
        burntAmount = 0;
        burning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (burning)
        {
            burntAmount += Time.deltaTime * burningSpeed;
        }
        mat.SetFloat("Vector1_2544001D", burntAmount);
        if (burntAmount >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FIRE"))
        {
            burning = true;
            other.gameObject.GetComponent<BaseSpirit>().MoveTo(other.gameObject.transform.position);
        }
    }
}
