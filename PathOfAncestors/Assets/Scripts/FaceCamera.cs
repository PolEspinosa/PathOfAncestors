using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private GameObject camera;
    private Vector3 faceDirection;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        faceDirection = new Vector3(camera.transform.position.x, gameObject.transform.position.y, camera.transform.position.z) - gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.LookRotation(faceDirection, Vector3.up);
        //if(gameObject.transform.eulerAngles.y <= 270 && gameObject.transform.eulerAngles.y >= 90)
        //{
            //gameObject.transform.localScale = new Vector3(1,1,-1);
            //Debug.Log("1");
        //}
        //else
        //{
            //gameObject.transform.localScale = new Vector3(1, 1, 1);
            //Debug.Log("2");
        //}
    }
}
