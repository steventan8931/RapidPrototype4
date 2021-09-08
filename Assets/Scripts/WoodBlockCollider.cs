using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlockCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            _other.GetComponent<Inventory>().m_WoodBlockCount++;
            Destroy(gameObject);
        }
    }
}
