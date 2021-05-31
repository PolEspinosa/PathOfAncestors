using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseUI, optionsUI;
    public CheckpointManager manager;
    public GameObject player;
    [SerializeField]
    private GameObject continueButton, restartButton, optionsButton, backToMenuButton, exitButton, optionsBackButton;
    [SerializeField]
    private Sprite continueAct, continueDeact, restartAct, restartDeact, optionsAct, optionsDeact, backToMenuAct, backToMenuDeact, exitAct, exitDeact, optionsBackAct, optionsBackDeact;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        optionsUI.SetActive(false);
        manager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        player = GameObject.Find("Character");
        continueButton.GetComponent<Image>().sprite = continueDeact;
        restartButton.GetComponent<Image>().sprite = restartDeact;
        //optionsButton.GetComponent<Image>().sprite = oprionsDeact;
        //backToMenuButton.GetComponent<Image>().sprite = backToMenuDeact;
        exitButton.GetComponent<Image>().sprite = exitDeact;
        gamePaused = false;
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
        optionsUI.SetActive(false);
        gamePaused = false;
        continueButton.GetComponent<Image>().sprite = continueDeact;
        restartButton.GetComponent<Image>().sprite = restartDeact;
        //optionsButton.GetComponent<Image>().sprite = optionsDeact;
        optionsBackButton.GetComponent<Image>().sprite = optionsBackDeact;
        //backToMenuButton.GetComponent<Image>().sprite = backToMenuDeact;
        exitButton.GetComponent<Image>().sprite = exitDeact;

    }

    public void ContinueAct()
    {
        continueButton.GetComponent<Image>().sprite = continueAct;
    }

    public void ContinueDeact()
    {
        continueButton.GetComponent<Image>().sprite = continueDeact;
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
        //SceneManager.LoadScene("Loading");
        player.transform.position = manager.actualCheckpoint.transform.position;
        pauseUI.SetActive(false);
        gamePaused = false;
        restartButton.GetComponent<Image>().sprite = restartDeact;
    }

    public void RestartAct()
    {
        restartButton.GetComponent<Image>().sprite = restartAct;
    }

    public void RestartDeact()
    {
        restartButton.GetComponent<Image>().sprite = restartDeact;
    }

    public void GoToOptions()
    {
        pauseUI.SetActive(false);
        optionsUI.SetActive(true);
        //optionsButton.GetComponent<Image>().sprite = optionsDeact;
    }

    public void GoToOptionsAct()
    {
        optionsButton.GetComponent<Image>().sprite = optionsAct;
    }

    public void GoToOptionsDeact()
    {
        optionsButton.GetComponent<Image>().sprite = optionsDeact;
    }

    public void OptionsBack()
    {
        pauseUI.SetActive(true);
        optionsUI.SetActive(false);
        optionsBackButton.GetComponent<Image>().sprite = optionsBackDeact;
    }

    public void OptionsBackAct()
    {
        optionsBackButton.GetComponent<Image>().sprite = optionsBackAct;
    }

    public void OptionsBackDeact()
    {
        optionsBackButton.GetComponent<Image>().sprite = optionsBackDeact;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMenuAct()
    {
        backToMenuButton.GetComponent<Image>().sprite = backToMenuAct;
    }

    public void BackToMenuDeact()
    {
        backToMenuButton.GetComponent<Image>().sprite = backToMenuDeact;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitGameAct()
    {
        exitButton.GetComponent<Image>().sprite = exitAct;
    }

    public void QuitGameDeact()
    {
        exitButton.GetComponent<Image>().sprite = exitDeact;
    }
}
