using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCrafting : MonoBehaviour
{
    [Header("Crafting Components")]
    public GameObject m_CraftingCanvas;
    public NewInventory m_Inventory;
    private BuildPlacement m_Builder;
    public int m_Cost = 0;
    public bool m_EnoughMaterials = false;
    string m_ItemName;

    public GameObject m_TurretOnePrefab;
    public GameObject m_TurretOneUI;
    public GameObject m_TurretTwoPrefab;
    public GameObject m_TurretTwoUI;

    private CamSwitcher m_CamSwitcher;
    private void Start()
    {
        m_Inventory = FindObjectOfType<NewInventory>();
        m_Builder = FindObjectOfType<BuildPlacement>();
        m_CamSwitcher = FindObjectOfType<CamSwitcher>();
    }

    private void UpdateSlot(int _ItemCost, GameObject _CraftUI)
    {
        if (m_Inventory.m_MagicCrystalCount >= _ItemCost)
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
        //Only Craft in Top Down
        if (m_CamSwitcher.m_IsFirstPerson)
        {
            m_CraftingCanvas.SetActive(false);
            return;
        }
        else
        {
            m_CraftingCanvas.SetActive(true);
        }

        //Order -> SetItemCostCount -> SetItemCost - > AddItem
        UpdateSlot(1, m_TurretOneUI);
        UpdateSlot(2, m_TurretTwoUI);
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
            case "m_Turret1":
                if (m_Inventory.m_MagicCrystalCount >= m_Cost)
                {
                    m_ItemName = _ItemName;
                    m_EnoughMaterials = true;
                }
                break;
            case "m_Turret2":
                if (m_Inventory.m_MagicCrystalCount >= m_Cost)
                {
                    m_ItemName = _ItemName;
                    m_EnoughMaterials = true;
                }
                break;
        }
    }

    public void AddItem()
    {
        if (m_EnoughMaterials)
        {
            switch (m_ItemName)
            {
                case "m_Turret1":
                    if (m_Builder.m_CurrentPlaceableObject == null)
                    {
                        m_Builder.m_CurrentPlaceableObject = Instantiate(m_TurretOnePrefab, Camera.main.transform);
                        m_Builder.m_CurrentPlaceableObject.transform.parent = null;
                        m_Builder.m_CanPlaceMat = m_Builder.m_CurrentPlaceableObject.transform.GetChild(0).GetComponent<Renderer>().material;
                        Debug.Log("added tur1");
                        m_EnoughMaterials = false;
                    }

                    break;
                case "m_Turret2":
                    if (m_Builder.m_CurrentPlaceableObject == null)
                    {
                        m_Builder.m_CurrentPlaceableObject = Instantiate(m_TurretTwoPrefab, Camera.main.transform);
                        m_Builder.m_CurrentPlaceableObject.transform.parent = null;
                        m_Builder.m_CanPlaceMat = m_Builder.m_CurrentPlaceableObject.transform.GetChild(0).GetComponent<Renderer>().material;
                        Debug.Log("added tur2");
                        m_EnoughMaterials = false;
                    }
                    break;
            }
        }
    }
}
