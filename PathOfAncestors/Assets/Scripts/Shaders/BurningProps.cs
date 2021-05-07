using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BurningProps : MonoBehaviour
{
    public Material mat;
    private Material matInstance;
    private float burntAmount;
    public bool burning;
    public ParticleSystem fireParticles;
    private ParticleSystem.MainModule particlesMainModule;
    [SerializeField]
    private float burntThreshold;
    [SerializeField]
    private float burningSpeed;
    

    public List<GameObject> nextBurnableObject;

    private bool playOnce;
    // Start is called before the first frame update
    void Start()
    {
        burntAmount = 0;
        burning = false;
        matInstance = new Material(mat);
        //mat = gameObject.GetComponent<MeshRenderer>().material;
        //particlesMainModule = fireParticles.main;
        fireParticles.Stop();
        playOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        //change material alpha amount
        matInstance.SetFloat("Vector1_54467BBE", burntAmount);
        if (burntAmount >= burntThreshold)
        {
            
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
        if (matInstance.GetFloat("Vector1_54467BBE") < 1 && burning)
            burntAmount += burningSpeed * Time.fixedDeltaTime;
        if (burning && playOnce)
        {
            playOnce = false;
            //play burning sound
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Mecanismos/burnWood", gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FIRE"))
        {
            this.GetComponent<MeshRenderer>().material = matInstance;
            Burn();
        }
    }

    public void Burn()
    {
        burning = true;
        //other.gameObject.GetComponent<BaseSpirit>().MoveTo(other.gameObject.transform.position);
        fireParticles.Play();
        if (nextBurnableObject.Count != 0)
        {
            StartCoroutine(BurnOtherObjects(.2f));
        }
    }

    IEnumerator BurnOtherObjects(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        foreach (GameObject burnableObject in nextBurnableObject)
        {
            if (burnableObject != null)
            {

                burnableObject.GetComponent<NewBurningObject>().Burn();
            }
        }
    }
}
