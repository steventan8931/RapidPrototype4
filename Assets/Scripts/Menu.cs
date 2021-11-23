using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuTab, tutorialTab;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene(2);
    }

    public void showTutorial()
    {
        menuTab.SetActive(false);
        tutorialTab.SetActive(true);
    }

    public void hideTutorial()
    {
        menuTab.SetActive(true);
        tutorialTab.SetActive(false);
    }
}
