using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardPlant : MonoBehaviour
{
    public float m_Damage = 5.0f;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.GetComponent<PlayerScr>() != null)
        {
            _other.gameObject.GetComponent<PlayerScr>().receiveDmg(m_Damage);
        }
    }
}
