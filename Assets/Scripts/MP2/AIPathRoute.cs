using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathRoute : MonoBehaviour
{
    public GameObject m_NextPostion;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<NewEnemyAI>() != null)
        {
            _other.GetComponent<NewEnemyAI>().m_AIStartPos = m_NextPostion;
        }
    }
}
