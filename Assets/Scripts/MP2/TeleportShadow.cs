using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportShadow : MonoBehaviour
{
    public GameObject m_StandardModel;

    private void Start()
    {
        m_StandardModel.SetActive(true);
    }

    public void CheckValid(bool _IsValid)
    {
        if (_IsValid)
        {
            m_StandardModel.SetActive(true);
        }
        else
        {
            m_StandardModel.SetActive(false);
        }
    }
}
