using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public GameObject m_CraftingUI;
    public bool m_CraftingOpen = false;
    public bool m_InRange = false;

    Inventory m_PlayerInventory;
    PlayerScr m_Player;

    AudioManager m_AudioManager;
    Crafting m_CraftManager;

    private void Start()
    {
        m_CraftingUI.SetActive(false);
        m_CraftingOpen = false;
        m_InRange = false;

        m_AudioManager = FindObjectOfType<AudioManager>();
        m_CraftManager = FindObjectOfType<Crafting>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            m_InRange = true;
            m_PlayerInventory = _other.GetComponent<Inventory>();
            m_Player = _other.GetComponent<PlayerScr>();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            m_InRange = false;
            m_CraftingOpen = false;
            _other.GetComponent<PlayerScr>().m_IsCrafting = false;
        }
    }   

    public void PlayCraftAnim()
    {
        if (m_CraftManager.m_EnoughMaterials)
        {
            m_Player.m_Animation.ResetTrigger("Crafting");
            m_Player.m_Animation.SetTrigger("Crafting");
            m_AudioManager.PlaySound("Craft");
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
            Cursor.visible = true;
            m_PlayerInventory.m_InventoryOpen = false;
            m_Player.m_IsCrafting = true;
        }
        else
        {
            Cursor.visible = false;
            m_CraftingUI.SetActive(false);
        }
    }
}
