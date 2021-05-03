using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBoxMaterial : MonoBehaviour
{
    private Renderer myRenderer;
    private Material defaultMat;
    [SerializeField]
    private Material highlightMat;
    private GameObject player;
    [SerializeField]
    private GameObject popUp;
    private SpiritsPassiveAbilities1 passiveScript;

    private void Awake()
    {
        player = GameObject.Find("Character");
    }
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        defaultMat = myRenderer.material;
        passiveScript = player.GetComponent<SpiritsPassiveAbilities1>();
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
        return !passiveScript.pushing && passiveScript.inRange;
    }

    private void OnDestroy()
    {
        Destroy(defaultMat);
    }
}
