using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBulletScr : MonoBehaviour
{
    public float damage = 35f;
    public float lifetime = 0;

    public GameObject m_HitFXPrefab;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        destroyAfterWhile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<NewEnemyAI>().currentHp -= damage;
            //collision.gameObject.GetComponent<NewEnemyAI>().receiveDmg(damage);
            collision.gameObject.GetComponent<NewEnemyAI>().receiveDmgPlayerBullet(damage, m_HitFXPrefab);
            collision.gameObject.GetComponent<NewEnemyAI>().caughtFire();
        }
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
}
