using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverCtrl : MonoBehaviour
{
    public GameObject FailUi, RetryButton, MenuButton;
    public BuddyScr Buddy;
    CharacterMotor m_Player;

    private void Start ()
    {
        Buddy = GameObject.FindGameObjectWithTag("Buddy").GetComponent<BuddyScr>();
        m_Player = FindObjectOfType<CharacterMotor>();
    }
    public void EnableFail()
    {
        if(Buddy.CurrHp <=0)
        {
            FailUi.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            m_Player.enabled = false;
        }
    }

    public void goMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        EnableFail();
    }
}
