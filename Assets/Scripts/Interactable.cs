using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header ("Interactable Components")]
    public int m_Health = 10;
    public Vector2 m_SpawnCountExtents = new Vector2(1, 1);
    public GameObject m_ParticlePrefab;

    public void TakeDamage(int _Damage)
    {
        if (m_Health > 0)
        {
            m_Health -= _Damage;
            Instantiate(m_ParticlePrefab, transform.position, Quaternion.identity);
        }
    }
}
