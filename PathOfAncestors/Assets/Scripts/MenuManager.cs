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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

}
