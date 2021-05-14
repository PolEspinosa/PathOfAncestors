using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeIncreaser());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TimeIncreaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            DataManager.totalTimePassed++;
        }
    }
}
