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
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        defaultMat = myRenderer.material;
        player = GameObject.FindGameObjectWithTag("Player");
        pickUp = player.GetComponentInChildren<PickUp>();
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
        return pickUp.objectToPickUp && !pickUp.hasObject; 
    }

    private void OnDestroy()
    {
        Destroy(defaultMat);
    }
}
