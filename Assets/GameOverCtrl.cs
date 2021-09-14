using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverCtrl : MonoBehaviour
{
    public GameObject FailUi, RetryButton, MenuButton;
    public PlayerScr player;
    private void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScr>();
    }
    public void EnableFail()
    {
        if(player.currenthitpoints <=0)
        {
            FailUi.SetActive(true);
            Cursor.visible = true;

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
