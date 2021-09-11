using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOut : MonoBehaviour
{
    public enum ActiveInHand
    {
        m_Melee,
        m_Wall,
        m_Trap,
    }

    public GameObject m_Melee;
    public GameObject m_Wall;
    public GameObject m_Trap;

    public ActiveInHand m_Hand;

    private void Update()
    {
        switch (m_Hand)
        {
            case ActiveInHand.m_Melee:
                m_Melee.SetActive(true);
                m_Wall.SetActive(false);
                m_Trap.SetActive(false);
                break;
            case ActiveInHand.m_Wall:
                m_Melee.SetActive(true);
                m_Wall.SetActive(true);
                m_Trap.SetActive(false);
                break;
            case ActiveInHand.m_Trap:
                m_Melee.SetActive(true);
                m_Wall.SetActive(false);
                m_Trap.SetActive(true);
                break;
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
