using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Resources Components")]
    public int m_RockBlockCount = 0;
    public int m_WoodBlockCount = 0;

    [Header("UI Components")]
    public GameObject m_InventoryCanvas;
    public bool m_InventoryOpen = false;
    public Text m_WoodCountUI;
    public Text m_RockCountUI;

    private void Start()
    {
        m_InventoryCanvas.SetActive(false);
        m_InventoryOpen = false;
    }

    private void Update()
    {
        m_WoodCountUI.text = m_WoodBlockCount.ToString();
        m_RockCountUI.text = m_RockBlockCount.ToString();

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_InventoryOpen = !m_InventoryOpen;

            if (m_InventoryOpen)
            {
                m_InventoryCanvas.SetActive(true);
                Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                m_InventoryCanvas.SetActive(false);
                Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.None;

            }
        }
    }
}
