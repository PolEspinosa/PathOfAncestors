using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool gamePaused = false;
    public GameObject pauseUI, optionsUI;
    public CheckpointManager manager;
    public GameObject player;
    [SerializeField]
    private GameObject continueButton, restartButton, optionsButton, backToMenuButton, exitButton;
    [SerializeField]
    private Sprite continueAct, continueDeact, restartAct, restartDeact, optionsAct, optionsDeact, backToMenuAct, backToMenuDeact, exitAct, exitDeact;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        optionsUI.SetActive(false);
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
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        gamePaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        gamePaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        player.transform.position = manager.actualCheckpoint.transform.position;
        pauseUI.SetActive(false);
        gamePaused = false;
    }

    public void GoToOptions()
    {
        pauseUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void OptionsBack()
    {
        pauseUI.SetActive(true);
        optionsUI.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
