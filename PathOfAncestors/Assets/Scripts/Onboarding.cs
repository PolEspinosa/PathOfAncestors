using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Onboarding : MonoBehaviour
{

    //fire
    public bool showFire = true;
    public bool isShowingFire = false;

    //aim
    public bool isShowingAim = false;

    //order
    public bool isShowingOrder = false;


    //images
    public GameObject fireTut;
    public GameObject aimTut;
    public GameObject orderTut;
    public GameObject earthTut;
    public GameObject interactTut;


    // Start is called before the first frame update
    void Start()
    {
        fireTut.SetActive(false);
        aimTut.SetActive(false);
        orderTut.SetActive(false);
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


    }

    private void OnTriggerEnter(Collider other)
    {
        if (showFire)
        {
            if (other.tag == "Player")
            {
                showFire = false;
                fireTut.SetActive(true);
                isShowingFire = true;

            }
        }
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
    }
}
