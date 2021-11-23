using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCrafting : MonoBehaviour
{
    [Header("Crafting Components")]
    public GameObject m_CraftingCanvas;
    public NewInventory m_Inventory;
    private BuildPlacement m_Builder;
    public int m_Cost = 0;
    public bool m_EnoughMaterials = false;
    string m_ItemName;

    [Header("UI + Prefabs Components")]
    public GameObject m_TurretOnePrefab;
    public GameObject m_TurretOneUI;
    public GameObject m_TurretTwoPrefab;
    public GameObject m_TurretTwoUI;
    public GameObject m_TurretThreePrefab;
    public GameObject m_TurretThreeUI;
    public GameObject m_TurretFourPrefab;
    public GameObject m_TurretFourUI;

    private CamSwitcher m_CamSwitcher;

    [Header("UI + Prefabs Components")]
    public AudioSource m_AudioSource;
    public AudioClip m_Click, m_Build;

    private void Start()
    {
        m_Inventory = FindObjectOfType<NewInventory>();
        m_Builder = FindObjectOfType<BuildPlacement>();
        m_CamSwitcher = FindObjectOfType<CamSwitcher>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void UpdateSlot(int _ItemCost, GameObject _CraftUI)
    {
        if (m_Inventory.m_MagicCrystalCount >= _ItemCost)
        {
            _CraftUI.SetActive(true);
            _CraftUI.transform.GetChild(0).GetComponent<Button>().interactable = true;
        }
        else
        {
            _CraftUI.transform.GetChild(0).GetComponent<Button>().interactable = false;
            //_CraftUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (!PauseCtrl.isPaused)
        {
            //Only Craft in Top Down
            if (m_CamSwitcher.m_IsFirstPerson)
            {
                m_CraftingCanvas.GetComponent<BuildCanvasTransition>().m_OnScreen = false;
                //m_CraftingCanvas.SetActive(false);
                return;
            }
            else
            {
                m_CraftingCanvas.GetComponent<BuildCanvasTransition>().m_OnScreen = true;
                //m_CraftingCanvas.SetActive(true);
            }

            //Order -> SetItemCostCount -> SetItemCost - > AddItem
            UpdateSlot(3, m_TurretOneUI);
            UpdateSlot(5, m_TurretTwoUI);
            UpdateSlot(5, m_TurretThreeUI);
            UpdateSlot(10, m_TurretFourUI);
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
            case "m_Turret3":
                if (m_Inventory.m_MagicCrystalCount >= m_Cost)
                {
                    m_ItemName = _ItemName;
                    m_EnoughMaterials = true;
                }
                break;
            case "m_Turret4":
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
                        //Disable turret to prevent shooting when not placed
                        m_Builder.m_CurrentPlaceableObject.GetComponent<TurretScr>().enabled = false;
                        m_Builder.m_CurrentPlaceableObject.transform.parent = null;
                        m_AudioSource.volume = 1.0f;
                        m_AudioSource.PlayOneShot(m_Click);
                        Debug.Log("added tur1");
                        m_EnoughMaterials = false;
                    }

                    break;
                case "m_Turret2":
                    if (m_Builder.m_CurrentPlaceableObject == null)
                    {
                        m_Builder.m_CurrentPlaceableObject = Instantiate(m_TurretTwoPrefab, Camera.main.transform);
                        //Disable turret to prevent shooting when not placed
                        m_Builder.m_CurrentPlaceableObject.GetComponent<TurretScr>().enabled = false;
                        m_Builder.m_CurrentPlaceableObject.transform.parent = null;
                        m_AudioSource.volume = 1.0f;
                        m_AudioSource.PlayOneShot(m_Click);
                        Debug.Log("added tur2");
                        m_EnoughMaterials = false;
                    }
                    break;
                case "m_Turret3":
                    if (m_Builder.m_CurrentPlaceableObject == null)
                    {
                        m_Builder.m_CurrentPlaceableObject = Instantiate(m_TurretThreePrefab, Camera.main.transform);
                        //Disable turret to prevent shooting when not placed
                        m_Builder.m_CurrentPlaceableObject.GetComponent<TurretScr>().enabled = false;
                        m_Builder.m_CurrentPlaceableObject.transform.parent = null;
                        m_AudioSource.volume = 1.0f;
                        m_AudioSource.PlayOneShot(m_Click);
                        Debug.Log("added tur3");
                        m_EnoughMaterials = false;
                    }
                    break;
                case "m_Turret4":
                    if (m_Builder.m_CurrentPlaceableObject == null)
                    {
                        m_Builder.m_CurrentPlaceableObject = Instantiate(m_TurretFourPrefab, Camera.main.transform);
                        //Disable turret to prevent shooting when not placed
                        m_Builder.m_CurrentPlaceableObject.GetComponent<TurretScr>().enabled = false;
                        m_Builder.m_CurrentPlaceableObject.transform.parent = null;
                        m_AudioSource.volume = 1.0f;
                        m_AudioSource.PlayOneShot(m_Click);
                        Debug.Log("added tur4");
                        m_EnoughMaterials = false;
                    }
                    break;
            }
        }
    }
}
