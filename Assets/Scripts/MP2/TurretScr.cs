﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScr : MonoBehaviour
{
    public Transform target;
    [Header("Attributes")]

    public float range = 20f;
    public float fireRate = 1f;
    public float fireRateStorage;
    protected float fireCountdown = 0f;
    public bool isBuffed = false;
    public float currBuffTime = 0f;
    public float MaxBuffTime = 5f;

    [Header("Unity setup fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 7f;

    public GameObject bulletPrefab;
    public Transform firepoint;
    
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        UpdateTarget();
        fireRateStorage = fireRate;
    }

    protected void UpdateTarget()
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
    public virtual void Update()
    {
        UpdateTarget();
        if(target == null)
        {
            return;
        }
        rotateToTarget();

        if(fireCountdown <= 0f)
        {
            CalBuff();
            Shoot();
            Debug.Log("fired turret ball");
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    protected void rotateToTarget()
    {
        Vector3 dir = target.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.AngleAxis(-90f, Vector3.up) * lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.AngleAxis(90f, Vector3.up) * lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void Shoot()
    {
        GameObject bulletgo = (GameObject)Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        TurretBullet bullet = bulletgo.GetComponent<TurretBullet>();
        CannonBullet canbullet = bulletgo.GetComponent<CannonBullet>();
        MortarBullet morbullet = bulletgo.GetComponent<MortarBullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }


        if (canbullet != null)
        {
            canbullet.Seek(target);
        }
        if(morbullet != null)
        {
            morbullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void CalBuff()
    {
        if(isBuffed == true)
        {
            fireRate = 1.5f * fireRateStorage;
            currBuffTime -= Time.deltaTime;
            if (currBuffTime <= 0)
            {
                currBuffTime = 0;
                isBuffed = false;
                fireRate = fireRateStorage;
            }
        }
        
    }
    public void buffTurret()
    {
        isBuffed = true;
        currBuffTime = MaxBuffTime;
    }
}
