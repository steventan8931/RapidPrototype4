using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFX : MonoBehaviour
{
    public float m_Timer = 0.0f;
    public float m_DespawnTime = 1.0f;

    public void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > m_DespawnTime)
        {
            Destroy(gameObject);
        }
    }
}
