using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightAltarMaterial : MonoBehaviour
{
    private Renderer myRenderer;
    private Material defaultMat;
    [SerializeField]
    private Material highlightMat;
    private GameObject player;
    private PickUp pickUp;
    [SerializeField]
    private GameObject popUp;
    private bool inRange;
    [SerializeField]
    private float interactDistance;
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
        return pickUp.hasObject && pickUp.inActivator && Vector3.Distance(gameObject.transform.position,player.transform.position) < interactDistance;
    }

    private void OnDestroy()
    {
        Destroy(defaultMat);
    }
}
