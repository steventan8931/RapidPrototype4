﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlockCollider : MonoBehaviour
{
    public GameObject m_Model;

    AudioManager m_AudioManager;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            _other.GetComponent<Inventory>().m_WoodBlockCount++;
            m_AudioManager.PlaySound("PickUp");
            Destroy(m_Model);
        }
    }
}
