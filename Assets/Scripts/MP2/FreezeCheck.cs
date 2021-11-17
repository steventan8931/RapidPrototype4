using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCheck : MonoBehaviour
{
    NewEnemyAI m_Enemy;
    public GameObject m_Ice;

    private void Start()
    {
        m_Enemy = transform.parent.GetComponent<NewEnemyAI>();
    }

    private void Update()
    {
        if (m_Enemy.onIce)
        {
            m_Ice.SetActive(true);
        }
        else
        {
            m_Ice.SetActive(false);
        }
    }
}
