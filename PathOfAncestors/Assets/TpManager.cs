using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tpObject;
    [SerializeField]
    private GameObject[] decoration;
    [SerializeField]
    private GameObject[] salas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            decoration[4].SetActive(true);
            MovePlayerToTp1();
            decoration[2].SetActive(false);
            decoration[3].SetActive(false);
            salas[0].SetActive(false);
            salas[13].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            decoration[13].SetActive(true);
            MovePlayerToTp2();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            decoration[16].SetActive(true);
            MovePlayerToTp3();
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            decoration[19].SetActive(true);
            MovePlayerToTp4();
            decoration[16].SetActive(false);
            decoration[17].SetActive(false);
            salas[7].SetActive(false);
            salas[8].SetActive(false);
        }
    }

    private void MovePlayerToTp1()
    {
        gameObject.transform.position = tpObject[0].transform.position;
    }

    private void MovePlayerToTp2()
    {
        gameObject.transform.position = tpObject[1].transform.position;
    }

    private void MovePlayerToTp3()
    {
        gameObject.transform.position = tpObject[2].transform.position;
    }

    private void MovePlayerToTp4()
    {
        gameObject.transform.position = tpObject[3].transform.position;
    }
}
