using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OrderSystem : MonoBehaviour
{
    public LayerMask ignoreMask;
    bool aiming = false;
    public GameObject aimCursor;
    public GameObject cursorReference;
    private Image cursorImage;
    [SerializeField]
    private Sprite defaultCursor, fireCursor, earthCursor;
    public RaycastHit hit;
    private Ray ray;
    public Vector3 goToPosition;
    public SpiritManager spiritManager;
    public Camera camera;

    public bool isGoingToEarth = false;
    public GameObject activator;
    //used to calculate if player has path to target
    private NavMeshPath playerPath;
    [SerializeField]
    private GameObject pathCalculator;
    private NavMeshAgent playerAgent;
    private NavMeshHit meshHit;

    //spirits sound instances
    private FMOD.Studio.EventInstance fireVoiceInstance, earthVoiceInstance;
    public float timesOrdered;

    // Start is called before the first frame update
    void Start()
    {
        aimCursor.SetActive(false);
        playerPath = new NavMeshPath();
        playerAgent = pathCalculator.GetComponent<NavMeshAgent>();
        cursorImage = aimCursor.GetComponent<Image>();
        fireVoiceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Voice/fireVoice");
        earthVoiceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Voice/voz tierra");
    }

    // Update is called once per frame
    void Update()
    {
        pathCalculator.transform.position = gameObject.transform.position;
        if (Input.GetMouseButton(1))
        {
            StartCoroutine(ManageCursorDelay());
        }
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
            StopCoroutine(ManageCursorDelay());
        }
        //cast the ray
        if (aiming && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(aimCursor.transform.position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreMask))
            {
                //deactivate and activate in order to update path calculator position
                pathCalculator.SetActive(false);
                pathCalculator.SetActive(true);
                //if there is no path, desinvoke earth spirit and invoke next to player
                if (spiritManager.currentSpirit.CompareTag("EARTH"))
                {
                    if (!spiritManager.currentSpirit.GetComponent<EarthSpirit>().HasPath(hit) && PlayerHasPath(hit) && !spiritManager.currentSpirit.GetComponent<EarthSpirit>().switchToSteering && !spiritManager.currentSpirit.GetComponentInChildren<SpiritsAnimatorController>().uninvoked)
                    {
                        spiritManager.Desinvoke(spiritManager.currentSpirit);
                        //invoke spirit and manage orders all at once
                        StartCoroutine(InvokeSpiritAgain(hit));
                    }
                    else
                    {
                        ManageOrders(hit);
                    }
                }
                else
                {
                    ManageOrders(hit);
                }
            }
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(Camera.main.transform.position, cursorReference.transform.position - Camera.main.transform.position, out hit, 100, ~ignoreMask))
            //{
            //    spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);
            //}
        }
        //if not aiming, make the spirit follow the player again
        else if (!aiming && Input.GetMouseButtonDown(0))
        {
            if (!PauseMenu.gamePaused)
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().ReturnToPlayer();

                isGoingToEarth = false;
                activator = null;
                ManageActivators();
                //play fire spirit sound if its not already playing
                if (spiritManager.currentSpirit.CompareTag("FIRE"))
                {
                    FMOD.Studio.PLAYBACK_STATE state;
                    fireVoiceInstance.getPlaybackState(out state);
                    if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fireVoiceInstance, spiritManager.currentSpirit.transform, spiritManager.currentSpirit.GetComponent<Rigidbody>());
                        fireVoiceInstance.start();
                    }
                }
                //play earth spirit sound if its not already playing
                else if (spiritManager.currentSpirit.CompareTag("EARTH"))
                {
                    FMOD.Studio.PLAYBACK_STATE state;
                    earthVoiceInstance.getPlaybackState(out state);
                    if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        FMODUnity.RuntimeManager.AttachInstanceToGameObject(earthVoiceInstance, spiritManager.currentSpirit.transform, spiritManager.currentSpirit.GetComponent<Rigidbody>());
                        earthVoiceInstance.start();
                    }
                }
            }
        }
    }

    public void ManageOrders(RaycastHit hit)
    {
        if (spiritManager.currentSpirit.CompareTag("EARTH"))
        {
            DataManager.totalTimesOrderedEarth++;
        }
        else if(spiritManager.currentSpirit.CompareTag("FIRE"))
        {
            DataManager.totalTimesOrderedFire++;
        }
        Debug.Log(hit.transform.tag);
        isGoingToEarth = false;
        activator = null;
        //get target tag
        spiritManager.currentSpirit.GetComponent<BaseSpirit>().targetTag = hit.transform.tag;
        //get target gameobject
        spiritManager.currentSpirit.GetComponent<BaseSpirit>().targetObject = hit.collider.gameObject;
        //play fire spirit sound if its not already playing
        if (spiritManager.currentSpirit.CompareTag("FIRE"))
        {
            FMOD.Studio.PLAYBACK_STATE state;
            fireVoiceInstance.getPlaybackState(out state);
            if(state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(fireVoiceInstance, spiritManager.currentSpirit.transform, spiritManager.currentSpirit.GetComponent<Rigidbody>());
                fireVoiceInstance.start();
            }
        }
        //play earth spirit sound if its not already playing
        else if (spiritManager.currentSpirit.CompareTag("EARTH"))
        {
            FMOD.Studio.PLAYBACK_STATE state;
            earthVoiceInstance.getPlaybackState(out state);
            if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(earthVoiceInstance, spiritManager.currentSpirit.transform, spiritManager.currentSpirit.GetComponent<Rigidbody>());
                earthVoiceInstance.start();
            }
        }

        if (hit.transform.tag == "Untagged" || hit.transform.CompareTag("PressurePlateActivator"))
        {
            //apply y offset to the fire spirit so it doesn't go through the target
            if (spiritManager.currentSpirit.CompareTag("FIRE"))
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(new Vector3(hit.point.x, hit.point.y + 0.75f, hit.point.z));
            }
            else
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);
            }
            ManageActivators();

        }
        else if (hit.transform.CompareTag("MovingPlatform"))
        {
            //apply y offset to the fire spirit so it doesn't go through the target
            if (spiritManager.currentSpirit.CompareTag("FIRE"))
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(new Vector3(hit.point.x, hit.point.y + 0.75f, hit.point.z));
            }
            else
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.collider.gameObject.transform.position);
            }
            ManageActivators();
        }
        else if (hit.transform.tag == "Torch")
        {
            if (spiritManager.activatorObject == null)
            {
                Transform pos = hit.transform.GetChild(0).transform;
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
            }
            else
            {
                ManageActivators();
            }

        }
        else if (hit.transform.tag == "OvenActivator")
        {
            activator = hit.transform.gameObject.transform.GetChild(0).gameObject;
            if (spiritManager.activatorObject == null)
            {
                Transform pos = hit.transform.GetChild(0).transform;
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
            }
            else
            {
                ManageActivators();
            }

        }



        else if (hit.transform.tag == "EarthPlatform")
        {
            float playerDistance = Vector3.Distance(this.transform.position, hit.transform.position);
            float earthDistance = Vector3.Distance(spiritManager.currentSpirit.gameObject.transform.position, hit.transform.position);

            if (playerDistance < earthDistance && !spiritManager.currentSpirit.GetComponent<EarthSpirit>().HasPath(hit))
            {
                spiritManager.Desinvoke(spiritManager.currentSpirit);
                StartCoroutine(InvokeSpiritAgain(hit));
            }
            isGoingToEarth = true;
            activator = hit.transform.gameObject.transform.GetChild(0).gameObject;
            if (spiritManager.activatorObject == null)
            {
                Transform pos = hit.transform.GetChild(0).transform;
                //spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
                //apply y offset to the fire spirit so it doesn't go through the target
                if (spiritManager.currentSpirit.CompareTag("FIRE"))
                {
                    spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(new Vector3(pos.position.x, pos.position.y + 1, pos.position.z));
                }
                else
                {
                    spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
                }
            }
            else
            {
                ManageActivators();
                Transform pos = hit.transform.GetChild(0).transform;
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);

            }
        }


        else if (hit.transform.tag == "BreakableWall")
        {
            float playerDistance = Vector3.Distance(this.transform.position, hit.transform.position);
            float earthDistance = Vector3.Distance(spiritManager.currentSpirit.gameObject.transform.position, hit.transform.position);

            //if (playerDistance < earthDistance && !spiritManager.currentSpirit.GetComponent<EarthSpirit>().HasPath(hit))
            //{
            //    spiritManager.Desinvoke(spiritManager.currentSpirit);
            //    StartCoroutine(InvokeSpiritAgain(hit));
            //}
            isGoingToEarth = true;
            if (spiritManager.activatorObject == null)
            {
                Transform pos = hit.transform.GetChild(0).transform;
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
            }
            else
            {
                Transform pos = hit.transform.GetChild(0).transform;
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
                ManageActivators();
            }

        }


        //else if (hit.transform.tag == "Oven")
        //{
        //    Transform pos = hit.transform.GetChild(0).transform;
        //    spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
        //}
        else if (hit.transform.CompareTag("Burnable"))
        {

            if (spiritManager.activatorObject == null)
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);
            }
            else
            {
                ManageActivators();
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);
            }
            
        }
    }

    void ManageActivators()
    {
        if (spiritManager.activatorObject != null)
        {
            if (spiritManager.activatorObject.gameObject.name == "Entry")
            {
                spiritManager.activatorObject.GetComponent<OvenActivator>().DeactivateOven();
            }
            else if (spiritManager.activatorObject.gameObject.name == "EarthPlatformActivator")
            {
                spiritManager.activatorObject.GetComponent<EarthPlatformActivator>().DeactivateEarthPlatform();
            }

        }
    }

    //this function will calculate if the player has a path to the target
    private bool PlayerHasPath(RaycastHit hit)
    {
        if (NavMesh.SamplePosition(hit.point, out meshHit, 5f, NavMesh.AllAreas))
        {
            playerAgent.CalculatePath(meshHit.position, playerPath);
        }
        if (playerPath.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        return true;
    }

    private IEnumerator InvokeSpiritAgain(RaycastHit hit)
    {
        yield return new WaitForSeconds(0.1f);
        spiritManager.InvokeSpirit(spiritManager.earthSpiritRef, spiritManager.earthPosition.transform);
        ManageOrders(hit);
    }

    private void ManageCursors()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(aimCursor.transform.position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreMask))
        {
            if (spiritManager.currentSpirit != null)
            {
                if (spiritManager.currentSpirit.CompareTag("FIRE"))
                {
                    if (hit.transform.CompareTag("Torch") || hit.transform.CompareTag("OvenActivator") || hit.transform.CompareTag("Burnable"))
                    {
                        cursorImage.sprite = fireCursor;
                    }
                    else
                    {
                        cursorImage.sprite = defaultCursor;
                    }
                }
                else if (spiritManager.currentSpirit.CompareTag("EARTH"))
                {
                    if (hit.transform.CompareTag("EarthPlatform") || hit.transform.CompareTag("BreakableWall"))
                    {
                        cursorImage.sprite = earthCursor;
                    }
                    else
                    {
                        cursorImage.sprite = defaultCursor;
                    }
                }
            }
            else
            {
                cursorImage.sprite = defaultCursor;
            }
        }
    }
    private IEnumerator ManageCursorDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            ManageCursors();
        }
    }

    private void OnDestroy()
    {
        fireVoiceInstance.release();
        earthVoiceInstance.release();
    }
}
