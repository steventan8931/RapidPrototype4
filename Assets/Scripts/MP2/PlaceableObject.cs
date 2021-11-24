using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public GameObject m_StandardModel;
    public GameObject m_InvalidPlacementModel;
    public GameObject m_InvalidPlacementCircle;
    public GameObject m_RangeCircle;
    private void Start()
    {
        m_StandardModel.SetActive(true);
        m_InvalidPlacementModel.SetActive(false);
    }

    public void CheckValid(bool _IsValid)
    {
        if (_IsValid)
        {
            m_StandardModel.SetActive(true);
            m_InvalidPlacementModel.SetActive(false);
        }
        else
        {
            m_StandardModel.SetActive(false);
            m_InvalidPlacementModel.SetActive(true);
        }
    }

}
