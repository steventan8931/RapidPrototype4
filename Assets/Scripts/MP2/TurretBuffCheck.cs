using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuffCheck : MonoBehaviour
{
    TurretScr m_Turret;
    public GameObject m_BuffVFX;

    private void Start()
    {
        m_Turret = transform.parent.GetComponent<TurretScr>();
    }

    private void Update()
    {
        if (m_Turret.isBuffed)
        {
            m_BuffVFX.SetActive(true);
        }
        else
        {
            m_BuffVFX.SetActive(false);
        }
    }
}
