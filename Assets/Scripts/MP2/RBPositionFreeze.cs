using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBPositionFreeze : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<NewEnemyAI>() != null)
        {
            _other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
