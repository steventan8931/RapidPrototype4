using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoseUIScr : MonoBehaviour
{
    public void backToMenu() 
    {
        SceneManager.LoadScene(0);
    }

    public void retry()
    {
        SceneManager.LoadScene(1);
    }
}
