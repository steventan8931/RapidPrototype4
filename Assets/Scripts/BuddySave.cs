using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddySave : MonoBehaviour
{
    public BuddyAnim m_Buddy;
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            if (_other.GetComponent<Inventory>().m_AntidoteBlockCount > 0)
            {
                m_Buddy.m_Saved = true;
            }
        }

    }
}
