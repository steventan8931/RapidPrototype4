using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScr : MonoBehaviour
{
    public int damage = 50;
    public float lifetime = 0;
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
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<NewEnemyAI>().currentHp -= damage;
            
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
