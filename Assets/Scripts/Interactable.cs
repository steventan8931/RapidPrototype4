using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header ("Interactable Components")]
    public int m_Health = 10;
    public Vector2 m_SpawnCountExtents = new Vector2(1, 1);
    public void TakeDamage(int _Damage)
    {
        m_Health -= _Damage;
    }
}
