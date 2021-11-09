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

    //Movement
    [SerializeField]
    private Transform target;
    public float speed = 50f;
    public GameObject impactEffect;

    private void Update()
    {
        m_LifeTimeCounter += Time.deltaTime;

        if (m_LifeTimeCounter >= m_LifeTime && !m_Exploded)
        {
            Explode();
        }

        if (m_Exploded)
        {
            GetComponent<Collider>().enabled = false;
            m_ExplodeLifeCounter += Time.deltaTime;

            if (m_ExplodeLifeCounter >= m_ExplodeLifeTime)
            {
                Destroy(gameObject);
            }
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            Debug.Log("exploded");
            Explode();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void Explode()
    {
        if (!m_Exploded)
        {         
            Collider[] hitobjects = Physics.OverlapSphere(transform.position, m_AttackRange, m_EnemyMask);
            Debug.Log("size  = " + hitobjects.Length);
            // Damage enemies
            foreach (Collider enemy in hitobjects)
            {
                if (enemy.GetComponent<NewEnemyAI>() != null)
                {
                    //damage Player
                    enemy.GetComponent<NewEnemyAI>().currentHp -= m_Damage;
                    GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
                    Destroy(effectIns, 2f);
                    //m_AudioManager.PlaySound("EnemyAttack");
                }
                //debug message
                Debug.Log(enemy.name + "has been hit");

            }

            //if (hitobjects.Length > 0)
            {
                //add animation here
                m_Exploded = true;
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
    }

    public void Seek(Transform tar)
    {
        target = tar;
    }

}
