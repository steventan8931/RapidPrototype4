using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScr : MonoBehaviour
{
    public Transform target;
    [Header("Attributes")]

    public float range = 20f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity setup fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 7f;

    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDist = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float disToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(disToEnemy < shortestDist)
            {
                shortestDist = disToEnemy;
                closestEnemy = enemy;
            }
        }
        if(closestEnemy != null && shortestDist <= range)
        {
            target = closestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        if(target == null)
        {
            return;
        }
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Debug.Log("shooted");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
