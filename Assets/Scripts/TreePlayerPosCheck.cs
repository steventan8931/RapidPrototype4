using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlayerPosCheck : MonoBehaviour
{
    public Vector3 m_PlayerPos;

    private void OnTriggerStay(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            m_PlayerPos = _other.transform.position;
        }
    }
}
