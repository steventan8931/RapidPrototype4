using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAxisChange : MonoBehaviour
{
    public float m_YChange = 0.0f;
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<NewEnemyAI>() != null)
        {
            // _other.transform.localPosition = new Vector3(_other.transform.localPosition.x, _other.transform.localPosition.y + m_YChange, _other.transform.localPosition.z);
            _other.GetComponent<NewEnemyAI>().LerpYChange(m_YChange);
        }
    }
}
