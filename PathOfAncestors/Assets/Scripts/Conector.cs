using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conector : MonoBehaviour
{

    public Activator activator;
    public Material deacMat;
    public Material actMat;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshRenderer>().material = deacMat;
    }

    // Update is called once per frame
    void Update()
    {
        if(!activator._activated)
        {
            this.GetComponent<MeshRenderer>().material = deacMat;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = actMat;
        }
    }
}
