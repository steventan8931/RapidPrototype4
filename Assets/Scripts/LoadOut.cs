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
        m_Trap,
    }

    public GameObject m_Fence;
    public GameObject m_Wall;
    public GameObject m_Trap;

    public ActiveInHand m_Hand;

    public GameObject m_FencePrefab;
    public GameObject m_WallPrefab;
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
                break;
            case ActiveInHand.m_Fence:
                if (m_Inventory.m_FenceBlockCount > 0)
                {
                    GetComponent<PlayerScr>().buildobj = m_FencePrefab;
                    GetComponent<PlayerScr>().isbuilder = true;
                    GetComponent<PlayerScr>().m_BuildMaterial = "m_FenceBlockCount";
                    m_Wall.SetActive(false);
                    m_Trap.SetActive(false);
                    m_Fence.SetActive(true);
                    break;
                }
                else
                {
                    goto case ActiveInHand.m_Wall;
                }
            case ActiveInHand.m_Wall:
                if (m_Inventory.m_WallBlockCount > 0)
                {
                    GetComponent<PlayerScr>().buildobj = m_WallPrefab;
                    GetComponent<PlayerScr>().isbuilder = true;
                    GetComponent<PlayerScr>().m_BuildMaterial = "m_WallBlockCount";
                    m_Wall.SetActive(true);
                    m_Trap.SetActive(false);
                    m_Fence.SetActive(false);
                    break;
                }
                else
                {
                    goto case ActiveInHand.m_Trap;
                }
            case ActiveInHand.m_Trap:
                if (m_Inventory.m_TrapBlockCount > 0)
                {
                    GetComponent<PlayerScr>().buildobj = m_TrapPrefab;
                    GetComponent<PlayerScr>().isbuilder = true;
                    GetComponent<PlayerScr>().m_BuildMaterial = "m_TrapBlockCount";
                    m_Wall.SetActive(false);
                    m_Trap.SetActive(true);
                    m_Fence.SetActive(false);
                    break;
                }
                else
                {
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
