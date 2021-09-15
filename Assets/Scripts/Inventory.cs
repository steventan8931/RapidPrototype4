using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Resources Components")]
    public int m_RockBlockCount = 0;
    public int m_WoodBlockCount = 0;
    public int m_RedStoneBlockCount = 0;
    public int m_BloodBlockCount = 0;
    public int m_FenceBlockCount = 0;
    public int m_WallBlockCount = 0;
    public int m_TrapBlockCount = 0;
    public int m_LavaTrapBlockCount = 0;
    public int m_AntidoteBlockCount = 0;

    [Header("UI Components")]
    public GameObject m_InventoryCanvas;
    public bool m_InventoryOpen = false;
    public GameObject m_WoodUI;
    public GameObject m_RockUI;
    public GameObject m_RedStoneUI;
    public GameObject m_FenceUI;
    public GameObject m_WallUI;
    public GameObject m_TrapUI;
    public GameObject m_LavaTrapUI;
    public GameObject m_AntidoteUI;
    public GameObject m_BloodUI;


    private void Start()
    {
        m_InventoryCanvas.SetActive(false);
        m_InventoryOpen = false;
    }

    public void UpdateInventorySlot(int _BlockCount, GameObject _BlockUI)
    {
        if (_BlockCount > 0)
        {
            _BlockUI.SetActive(true);
            _BlockUI.GetComponentInChildren<Text>().text = _BlockCount.ToString();
        }
        else
        {
            _BlockUI.SetActive(false);
        }
    }

    public void Add(Inventory _try)
    {

    }
    private void Update()
    {
        UpdateInventorySlot(m_RockBlockCount, m_RockUI);
        UpdateInventorySlot(m_WoodBlockCount, m_WoodUI);
        UpdateInventorySlot(m_RedStoneBlockCount, m_RedStoneUI);
        UpdateInventorySlot(m_FenceBlockCount, m_FenceUI);
        UpdateInventorySlot(m_WallBlockCount, m_WallUI);
        UpdateInventorySlot(m_TrapBlockCount, m_TrapUI);
        UpdateInventorySlot(m_LavaTrapBlockCount, m_LavaTrapUI);
        UpdateInventorySlot(m_AntidoteBlockCount, m_AntidoteUI);
        UpdateInventorySlot(m_BloodBlockCount, m_BloodUI);

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_InventoryOpen = !m_InventoryOpen;
            if (!m_InventoryOpen)
            {
                Cursor.visible = false;
            }
        }

        if (m_InventoryOpen)
        {
            m_InventoryCanvas.SetActive(true);
            Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            m_InventoryCanvas.SetActive(false);

            //Cursor.lockState = CursorLockMode.None;

        }
    }
}
