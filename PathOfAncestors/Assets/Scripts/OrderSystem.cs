using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{

    public LayerMask ignoreMask;
    bool aiming = false;
    public GameObject aimCursor;
    public GameObject cursorReference;
    
    public RaycastHit hit;
    private Ray ray;
    public Vector3 goToPosition;
    public SpiritManager spiritManager;
    public Camera camera;
   


    // Start is called before the first frame update
    void Start()
    {
        aimCursor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;
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

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);

              
            }
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(Camera.main.transform.position, cursorReference.transform.position - Camera.main.transform.position, out hit, 100, ~ignoreMask))
            //{
            //    spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);
            //}
        }
    }
}
