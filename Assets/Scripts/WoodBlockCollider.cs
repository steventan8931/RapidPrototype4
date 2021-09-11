using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlockCollider : MonoBehaviour
{
    public GameObject m_Model;
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            _other.GetComponent<Inventory>().m_WoodBlockCount++;
            Destroy(m_Model);
        }
    }
}
