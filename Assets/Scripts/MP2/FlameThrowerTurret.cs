using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerTurret : TurretScr
{
    [Header("Flamethrower components")]
    public GameObject FlameThrower;
    public float m_FlameDuration = 5.0f;
    public float m_ReloadDuration = 2.5f;
    public float m_BuffedFlameDuration = 10.0f;
    public float m_BuffedReloadDuration = 1.0f;

    private float reloadCountdown = 0f;

    private float cacheFlameDuration = 0.0f;
    private float cacheReloadDuration = 0.0f;

    public AudioSource m_Audio;

    void Start()
    {
        cacheFlameDuration = m_FlameDuration;
        cacheReloadDuration = m_ReloadDuration;

        UpdateTarget();
        FlameThrower.SetActive(false);

        m_Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Update()
    {
        UpdateTarget();
        BuffTurret();
        if (target == null)
        {
            FlameThrower.SetActive(false);
            m_Audio.Stop();
            return;
        }
        else
        {
            if (!m_Audio.isPlaying)
            {
                m_Audio.Play();
            }
        }

        fireCountdown += Time.deltaTime;

        rotateToTarget();

        if (fireCountdown <= m_FlameDuration)
        {
            NewShoot();
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

    public void BuffTurret()
    {
        if (isBuffed)
        {
            m_FlameDuration = m_BuffedFlameDuration;
            m_ReloadDuration = m_BuffedReloadDuration;
        }  
        else
        {
            m_FlameDuration = cacheFlameDuration;
            m_ReloadDuration = cacheReloadDuration;
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
