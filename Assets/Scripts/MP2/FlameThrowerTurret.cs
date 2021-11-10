using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerTurret : TurretScr
{
    [Header("Flamethrower components")]
    public GameObject FlameThrower;
    public float m_FlameDuration = 5.0f;
    public float m_ReloadDuration = 2.5f;

    private float reloadCountdown = 0f;

    void Start()
    {
        UpdateTarget();
        FlameThrower.SetActive(false);
    }

    // Update is called once per frame
    public override void Update()
    {
        UpdateTarget();
        if (target == null)
        {
            Debug.Log("no turret");
            return;
        }
        else
        {
            Debug.Log("enemy found");

        }

        fireCountdown += Time.deltaTime;

        rotateToTarget();

        if (fireCountdown <= m_FlameDuration)
        {
            NewShoot();
            Debug.Log("fired turret ball");

        }
        else
        {
            reloadCountdown += Time.deltaTime;
            if (reloadCountdown >= m_ReloadDuration)
            {
                fireCountdown = 0;
                reloadCountdown = 0;
            }
            FlameThrower.SetActive(false);
        }

    }
    //private void rotateToTarget()
    //{
    //    Vector3 dir = target.position - transform.position;

    //    Quaternion lookRotation = Quaternion.LookRotation(dir);

    //    Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.AngleAxis(-90f, Vector3.up) * lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    //    partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    //}
    private void NewShoot()
    {
        FlameThrower.SetActive(true);
    }

}
