
using System;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private Transform target;
    public float lifetime = 0f;
    public float speed = 50f;
    public GameObject impactEffect;
    public float dmg = 25f;
    public void Seek(Transform tar)
    {
        target = tar;
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        destroyAfterWhile();
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

        
    }

    private void HitTarget()
    {
        //Debug.Log("hit something");
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Damage(target);
        Destroy(effectIns, 2f);
        Destroy(gameObject);
    }

    void destroyAfterWhile()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= 7f)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Damage(Transform enemy)
    {
        NewEnemyAI ene = enemy.GetComponent<NewEnemyAI>();

        if (ene != null)
        {
            ene.receiveDmg(dmg);
        }
    }
}
