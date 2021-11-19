using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDisable : MonoBehaviour
{
    public float m_Timer = 0.0f;
    public float m_EndTimer = 0.0f;
    public GameObject m_SetCamActive;
    private void LateUpdate()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > m_EndTimer)
        {
            if (m_SetCamActive)
            {
                m_SetCamActive.SetActive(true);
            }
            gameObject.SetActive(false);           
        }
    }
}
