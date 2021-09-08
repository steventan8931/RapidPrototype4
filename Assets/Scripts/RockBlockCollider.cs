using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlockCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            _other.GetComponent<Inventory>().m_RockBlockCount++;
            Destroy(gameObject);
        }
    }
}
