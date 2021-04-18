using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool gamePaused = false;
    public GameObject pauseUI;
    public CheckpointManager manager;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        manager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        if(!gamePaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Continue();
            }

            else
            {
                Pause();
            }
        }
    }



    public void Continue()
    {
        pauseUI.SetActive(false);
        gamePaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        gamePaused = true;
    }

    public void Restart()
    {
        player.transform.position = manager.actualCheckpoint.transform.position;
        pauseUI.SetActive(false);
        gamePaused = false;
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
