using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOut : MonoBehaviour
{
    public enum ActiveInHand
    {
        m_Melee,
        m_Fence,
        m_Wall,
        m_Decoy,
        m_LavaTrap,
        m_Trap,
    }

    public GameObject m_Fence;
    public GameObject m_Wall;
    public GameObject m_LavaTrap;
    public GameObject m_Trap;
    public GameObject m_Decoy;

    public ActiveInHand m_Hand;

    public GameObject m_FencePrefab;
    public GameObject m_WallPrefab;
    public GameObject m_DecoyPrefab;
    public GameObject m_LavaTrapPrefab;
    public GameObject m_TrapPrefab;

    Inventory m_Inventory;

    private void Start()
    {
        m_Inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        switch (m_Hand)
        {
            case ActiveInHand.m_Melee:
                GetComponent<PlayerScr>().isbuilder = false;
                m_Wall.SetActive(false);
                m_Trap.SetActive(false);
                m_Fence.SetActive(false);
                m_Decoy.SetActive(false);
                m_LavaTrap.SetActive(false);
                break;
            case ActiveInHand.m_Fence:
                if (m_Inventory.m_FenceBlockCount > 0)
                {
                    GetComponent<PlayerScr>().buildobj = m_FencePrefab;
                    GetComponent<PlayerScr>().isbuilder = true;
                    GetComponent<PlayerScr>().m_BuildMaterial = "m_FenceBlockCountUse";
                    m_Wall.SetActive(false);
                    m_Trap.SetActive(false);
                    m_LavaTrap.SetActive(false);
                    m_Decoy.SetActive(false);
                    m_Fence.SetActive(true);
                    break;
                }
                else
                {
                    m_Hand = ActiveInHand.m_Wall;
                    goto case ActiveInHand.m_Wall;
                }
            case ActiveInHand.m_Wall:
                if (m_Inventory.m_WallBlockCount > 0)
                {
                    GetComponent<PlayerScr>().buildobj = m_WallPrefab;
                    GetComponent<PlayerScr>().isbuilder = true;
                    GetComponent<PlayerScr>().m_BuildMaterial = "m_WallBlockCountUse";
                    m_Wall.SetActive(true);
                    m_Trap.SetActive(false);
                    m_LavaTrap.SetActive(false);
                    m_Decoy.SetActive(false);
                    m_Fence.SetActive(false);
                    break;
                }
                else
                {
                    m_Hand = ActiveInHand.m_Decoy;
                    goto case ActiveInHand.m_Decoy;
                }
            case ActiveInHand.m_Decoy:
                if (m_Inventory.m_DecoyBlockCount > 0)
                {
                    GetComponent<PlayerScr>().buildobj = m_DecoyPrefab;
                    GetComponent<PlayerScr>().isbuilder = true;
                    GetComponent<PlayerScr>().m_BuildMaterial = "m_DecoyBlockCountUse";
                    m_Wall.SetActive(false);
                    m_Trap.SetActive(false);
                    m_LavaTrap.SetActive(false);
                    m_Decoy.SetActive(true);
                    m_Fence.SetActive(false);
                    break;
                }
                else
                {
                    m_Hand = ActiveInHand.m_LavaTrap;
                    goto case ActiveInHand.m_LavaTrap;
                }
            case ActiveInHand.m_LavaTrap:
                if (m_Inventory.m_LavaTrapBlockCount > 0)
                {
                    GetComponent<PlayerScr>().buildobj = m_LavaTrapPrefab;
                    GetComponent<PlayerScr>().isbuilder = true;
                    GetComponent<PlayerScr>().m_BuildMaterial = "m_LavaTrapBlockCountUse";
                    m_Wall.SetActive(false);
                    m_LavaTrap.SetActive(true);
                    m_Trap.SetActive(false);
                    m_Decoy.SetActive(false);
                    m_Fence.SetActive(false);
                    break;
                }
                else
                {
                    m_Hand = ActiveInHand.m_Trap;
                    goto case ActiveInHand.m_Trap;
                }
            case ActiveInHand.m_Trap:
                if (m_Inventory.m_TrapBlockCount > 0)
                {
                    GetComponent<PlayerScr>().buildobj = m_TrapPrefab;
                    GetComponent<PlayerScr>().isbuilder = true;
                    GetComponent<PlayerScr>().m_BuildMaterial = "m_TrapBlockCountUse";
                    m_Wall.SetActive(false);
                    m_LavaTrap.SetActive(false);
                    m_Trap.SetActive(true);
                    m_Fence.SetActive(false);
                    break;
                }
                else
                {
                    m_Hand = ActiveInHand.m_Melee;
                    goto case ActiveInHand.m_Melee;
                }
        }
        if (m_Hand < ActiveInHand.m_Melee)
        {
            m_Hand = ActiveInHand.m_Trap;
        }
        if (m_Hand > ActiveInHand.m_Trap)
        {
            m_Hand = ActiveInHand.m_Melee;
        }
        if (-Input.mouseScrollDelta.y > 0)
        {
            m_Hand++;
        }
        if (-Input.mouseScrollDelta.y < 0)
        {
            m_Hand--;
        }
    }
}
