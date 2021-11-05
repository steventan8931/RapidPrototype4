using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTurretScr : MonoBehaviour
{
    public Transform target;
    [Header("Attributes")]

    public float range = 8f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float currentFireCount = 3f;
    public float MaxFireCount = 3f;

    [Header("Unity setup fields")]
    public string enemyTag = "Enemy";
    public LayerMask enemyMask;
    public Transform partToRotate;
    public float turnSpeed = 7f;

    public GameObject bulletPrefab;
    public Transform firepoint;
    public bool enemyInFireRange;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        UpdateTarget();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDist = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float disToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (disToEnemy < shortestDist)
            {
                shortestDist = disToEnemy;
                closestEnemy = enemy;
            }
        }
        if (closestEnemy != null && shortestDist <= range)
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
        if (target == null)
        {
            return;
        }
        rotateToTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            Debug.Log("fired turret ball");
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    private void rotateToTarget()
    {
        Vector3 dir = target.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.AngleAxis(-90f, Vector3.up) * lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void Shoot()
    {
        GameObject bulletgo = (GameObject)Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        TurretBullet bullet = bulletgo.GetComponent<TurretBullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
