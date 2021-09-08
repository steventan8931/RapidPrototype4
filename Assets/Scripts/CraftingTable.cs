using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public GameObject m_CraftingUI;
    public bool m_CraftingOpen = false;
    public bool m_InRange = false;

    private void Start()
    {
        m_CraftingUI.SetActive(false);
        m_CraftingOpen = false;
        m_InRange = false;
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            m_InRange = true;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            m_InRange = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && m_InRange)
        {
            m_CraftingOpen = !m_CraftingOpen;
        }
        if (m_CraftingOpen)
        {
            m_CraftingUI.SetActive(true);
        }
        else
        {
            m_CraftingUI.SetActive(false);
        }
    }
}
