﻿using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        defaultMat = myRenderer.material;
        player = GameObject.Find("Character");
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
        return pickUp.hasObject && pickUp.inActivator;
    }

    private void OnDestroy()
    {
        Destroy(defaultMat);
    }
}