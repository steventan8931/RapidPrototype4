﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScr : MonoBehaviour
{
    public float damage = 50f;
    public float lifetime = 0;

    public GameObject m_HitFXPrefab;

    // Update is called once per frame
    void Update()
    {
        destroyAfterWhile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<NewEnemyAI>().currentHp -= damage;
            //collision.gameObject.GetComponent<NewEnemyAI>().receiveDmg(damage);
            collision.gameObject.GetComponent<NewEnemyAI>().receiveDmgPlayerBullet(damage, m_HitFXPrefab);
        }
        Destroy(gameObject);
    }
    void destroyAfterWhile()
    {
        lifetime += Time.deltaTime;
        if(lifetime >= 7f)
        {
            Destroy(gameObject);
            return;
        }
    }
}
