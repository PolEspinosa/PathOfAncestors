using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Onboarding : MonoBehaviour
{


    public bool tutorial=false;
    public bool interact=false;
    public bool earth=false;
    //fire
    public bool showFire = true;
    public bool isShowingFire = false;

    //aim
    public bool isShowingAim = false;

    //order
    public bool isShowingOrder = false;

    //interact
    public bool isShowingInteract = false;

    //earth
    public bool isShowingEarth = false;

    //images
    public GameObject fireTrust;
    public GameObject earthTrust;
    public GameObject fireTut;
    public GameObject aimTut;
    public GameObject orderTut;
    public GameObject followTut;
    public GameObject earthTut;
    public GameObject interactTut;


    // Start is called before the first frame update
    void Start()
    {
        fireTrust.SetActive(false);
        earthTrust.SetActive(false);
        fireTut.SetActive(false);
        aimTut.SetActive(false);
        orderTut.SetActive(false);
        followTut.SetActive(false);
        earthTut.SetActive(false);
        interactTut.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (isShowingFire && Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(CloseFireTut(.5f));

        }

        if(isShowingAim && Input.GetMouseButtonDown(1))
        {
            Debug.Log("a");
            StartCoroutine(CloseAimTut(.5f));
        }

        if (isShowingOrder && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CloseControls(.5f));
        }

        if(isShowingInteract && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CloseInteract(.5f));
        }

        if(isShowingEarth && Input.GetKeyDown(KeyCode.Alpha2))
        {

            StartCoroutine(CloseEarthTut(.5f));
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        

        if(interact)
        {
            if (other.tag == "Player")
            {
                interactTut.SetActive(true);
                isShowingInteract = true;
            }
        }

       
    }
    public void ActiveFireTut()
    {
        fireTrust.SetActive(true);
        StartCoroutine(StartFireTut(2f));
    }

    public void ActiveEartTut()
    {
        earthTrust.SetActive(true);
        StartCoroutine(StartEarthTut(2f));

    }



    IEnumerator StartFireTut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fireTrust.SetActive(false);
        fireTut.SetActive(true);
        isShowingFire = true;
    }

    IEnumerator StartEarthTut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        earthTrust.SetActive(false);
        earthTut.SetActive(true);
        isShowingEarth = true;
    }

    IEnumerator CloseFireTut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fireTut.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        aimTut.SetActive(true);
        isShowingFire = false;
        isShowingAim = true;
    }

    IEnumerator CloseAimTut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        aimTut.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        orderTut.SetActive(true);
        isShowingAim = false;
        isShowingOrder = true;
        
    }


    IEnumerator CloseControls(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        orderTut.SetActive(false);
        isShowingOrder = false;
        StartCoroutine(ShowFollow(4f));
    }

    IEnumerator ShowFollow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        followTut.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        followTut.SetActive(false);
        Destroy(this.gameObject);
    }

    IEnumerator CloseInteract(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        interactTut.SetActive(false);
        Destroy(this.gameObject);
    }

    IEnumerator CloseEarthTut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        earthTut.SetActive(false);
        Destroy(this.gameObject);
    }
}
