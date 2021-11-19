using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
    public GameObject m_Player;
    public GameObject m_CraftHUD;
    public GameObject m_HUD;
    public GameObject m_WaveManager;

    public GameObject m_CutSceneCamera;

    private void Start()
    {
        m_Player.SetActive(false);
        m_CraftHUD.SetActive(false);
        m_HUD.SetActive(false);
        m_WaveManager.SetActive(false);
        m_CutSceneCamera.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void LoadPlayerObjects()
    {
        m_Player.SetActive(true);
        m_CraftHUD.SetActive(true);
        m_HUD.SetActive(true);
        m_WaveManager.SetActive(true);
        m_CutSceneCamera.SetActive(false);
    }
}
