using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerFireDamage : MonoBehaviour
{
    public float m_Damage;

    private void OnTriggerStay(Collider _other)
    {
        if (_other.GetComponent<NewEnemyAI>() != null)
        {
            //_other.gameObject.GetComponent<NewEnemyAI>().receiveDmg(m_Damage);
            //IF not on fire burn them
            //if (!_other.gameObject.GetComponent<NewEnemyAI>().onFire)
            {
                _other.gameObject.GetComponent<NewEnemyAI>().caughtFire();
            }

        }
    }
}
