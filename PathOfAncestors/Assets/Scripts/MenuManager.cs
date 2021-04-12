using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //start
    public Button startButton;
    public Sprite startButtonDeact;
    public Sprite startButtonAct;

    //exit
    public Button exitButton;
    public Sprite exitButtonDeact;
    public Sprite exitButtonAct;

    //credits
    public Button creditsButton;
    public Sprite creditsAct;
    public Sprite creditsDeact;

    //back
    public Button backButton;
    public Sprite backAct;
    public Sprite backDeact;



    public GameObject creditsUI;
    public GameObject menu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        creditsUI.SetActive(false);
    }

    public void ActiveStart()
    {
        startButton.GetComponent<Image>().sprite = startButtonAct;
    }

    public void DeactiveStart()
    {
        startButton.GetComponent<Image>().sprite = startButtonDeact;
    }

    public void ActiveExit()
    {
        exitButton.GetComponent<Image>().sprite = exitButtonAct;
    }

    public void DeactiveExit()
    {
        exitButton.GetComponent<Image>().sprite = exitButtonDeact;
    }

    public void ActiveCredits()
    {
        creditsButton.GetComponent<Image>().sprite = creditsAct;
    }

    public void DeactivateCredits()
    {
        creditsButton.GetComponent<Image>().sprite = creditsDeact;
    }

    public void ActiveBack()
    {
        backButton.GetComponent<Image>().sprite = backAct;
    }

    public void DeactiveBack()
    {
        backButton.GetComponent<Image>().sprite = backDeact;
    }




    public void ShowCredits()
    {
        creditsUI.SetActive(true);
        menu.SetActive(false);
    }

    public void ShowMenu()
    {
        creditsUI.SetActive(false);
        menu.SetActive(true);
    }
}
