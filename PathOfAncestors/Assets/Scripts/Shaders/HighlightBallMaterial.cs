using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBallMaterial : MonoBehaviour
{
    private Renderer myRenderer;
    private Material defaultMat;
    [SerializeField]
    private Material highlightMat;
    private GameObject player;
    [SerializeField]
    private GameObject popUp;
    private PickUp pickUp;
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");
        myRenderer = gameObject.GetComponent<Renderer>();
        defaultMat = myRenderer.material;
        pickUp = player.GetComponentInChildren<PickUp>();
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanInteract())
        {
            myRenderer.material = highlightMat;
            popUp.SetActive(true);
        }
        else
        {
            myRenderer.material = defaultMat;
            popUp.SetActive(false);
        }
    }

    private bool CanInteract()
    {
        return pickUp.objectToPickUp && !pickUp.hasObject && inRange; 
    }

    private void OnDestroy()
    {
        Destroy(defaultMat);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUpTrigger"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickUpTrigger"))
        {
            inRange = false;
        }
    }
}
