using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCtrl : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject Pausemenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
    }

    public void checkInput()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            if(isPaused == false)
            {
                pauseGame();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                resumeGame();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }
    public void  pauseGame()
    {
        isPaused = true;
        Pausemenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void resumeGame()
    {
        isPaused = false;
        Pausemenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
