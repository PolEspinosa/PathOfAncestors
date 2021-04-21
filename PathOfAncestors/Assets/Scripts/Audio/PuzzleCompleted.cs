using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompleted : MonoBehaviour
{
    [SerializeField]
    private GroupActivator groupActivator;
    private MusicManager musicManager;
    private bool playOnce;
    // Start is called before the first frame update
    void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        playOnce = true;
        musicManager.puzzleCompleted = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playOnce)
        {
            if (groupActivator._activated)
            {
                musicManager.puzzleCompleted = 1;
                playOnce = false;
                StartCoroutine(StopSound());
            }
        }
    }

    private IEnumerator StopSound()
    {
        yield return new WaitForSeconds(2f);
        musicManager.puzzleCompleted = 0;
    }
}
