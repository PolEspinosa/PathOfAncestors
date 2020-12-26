using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{

    public LayerMask ignoreMask;
    bool aiming = false;
    public GameObject aimCursor;
    public RaycastHit hit;
    private Ray ray;
    public Vector3 goToPosition;
    public SpiritManager spiritManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            aiming = true;
            //Cursor.visible = true;
            aimCursor.SetActive(true);
        }
        //stop aiming
        else if (Input.GetMouseButtonUp(1))
        {
            aiming = false;
            //Cursor.visible = false;
            aimCursor.SetActive(false);
        }
        //cast the ray
        if (aiming && Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(Camera.main.transform.position, aimCursor.transform.position - Camera.main.transform.position, out hit, 100, ~ignoreMask))
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);
            }
        }
        //if not aiming, make the spirit follow the player again
        else if(!aiming && Input.GetMouseButtonDown(0))
        {
            spiritManager.currentSpirit.GetComponent<BaseSpirit>().ReturnToPlayer();
        }
    }
}
