using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [Header("Crafting Components")]
    public Inventory m_Inventory;
    public int m_Cost = 0;
    public bool m_EnoughMaterials = false;

    [Header("Interactable Components")]
    public GameObject m_FenceCraftUI;

    private void Start()
    {
        m_Inventory = FindObjectOfType<Inventory>();
    }

    public void UpdateCraftingSlot(int _BlockCount, int _ItemCost, GameObject _CraftUI)
    {
        if (_BlockCount >= _ItemCost)
        {
            _CraftUI.SetActive(true);
        }
        else
        {
            _CraftUI.SetActive(false);
        }
    }

    private void Update()
    {
        //ItemCostCount
        //ItemCost
        //Add Item

        UpdateCraftingSlot(m_Inventory.m_WoodBlockCount, 10, m_FenceCraftUI);
    }

    //Button Functions
    public void AddItem(string _ItemName)
    {
        if (m_EnoughMaterials)
        {
            switch (_ItemName)
            {
                case "m_RockBlockCount":
                    m_Inventory.m_RockBlockCount++;
                    break;
                case "m_WoodBlockCount":
                    m_Inventory.m_WoodBlockCount++;
                    break;
                case "m_FenceBlockCount":
                    m_Inventory.m_FenceBlockCount++;
                    Debug.Log("added fence");
                    break;
            }
        }
    }
    public void SetItemCostCount(int _ItemCostCount)
    {
        m_Cost = _ItemCostCount;
        m_EnoughMaterials = false;
    }
    public void SetItemCost(string _ItemName)
    {
        switch (_ItemName)
        {
            case "m_RockBlockCount":
                if (m_Inventory.m_RockBlockCount >= m_Cost)
                {
                    m_Inventory.m_RockBlockCount -= m_Cost;
                    m_EnoughMaterials = true;
                }
                break;
            case "m_WoodBlockCount":
                if (m_Inventory.m_WoodBlockCount >= m_Cost)
                {
                    m_Inventory.m_WoodBlockCount -= m_Cost;
                    m_EnoughMaterials = true;
                }
                break;
            case "m_FenceBlockCount":
                if (m_Inventory.m_FenceBlockCount >= m_Cost)
                {
                    m_Inventory.m_FenceBlockCount -= m_Cost;
                    m_EnoughMaterials = true;
                }
                break;
        }
    }
}
