using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    //Bullet
    public int m_Damage = 100;
    public float m_LifeTime = 10.0f;
    private float m_LifeTimeCounter = 0.0f;

    //Explosion
    public LayerMask m_EnemyMask;
    public float m_AttackRange = 1.0f;
    public bool m_Exploded = false;
    public float m_ExplodeLifeTime = 1.0f;
    private float m_ExplodeLifeCounter = 0.0f;

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.tag == "Enemy")
        {
            Explode();
        }
    }

    private void Update()
    {
        m_LifeTimeCounter += Time.deltaTime;

        if (m_LifeTimeCounter >= m_LifeTime && !m_Exploded)
        {
            Explode();
        }


        if (m_Exploded)
        {
            m_ExplodeLifeCounter += Time.deltaTime;

            if (m_ExplodeLifeCounter >= m_ExplodeLifeTime)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Explode()
    {
        if (!m_Exploded)
        {
            Debug.Log("exploded");
            Collider[] hitobjects = Physics.OverlapSphere(transform.position, m_AttackRange, m_EnemyMask);
            // Damage enemies
            foreach (Collider enemy in hitobjects)
            {
                if (enemy.GetComponent<NewEnemyAI>() != null)
                {
                    //damage Player
                    enemy.GetComponent<NewEnemyAI>().currentHp -= m_Damage;
                    //m_AudioManager.PlaySound("EnemyAttack");
                }
                //debug message
                Debug.Log(enemy.name + "has been hit");

            }

            //add animation here

            m_Exploded = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
    }
}
