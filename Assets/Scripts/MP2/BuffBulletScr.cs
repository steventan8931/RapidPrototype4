using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBulletScr : MonoBehaviour
{
    
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
        if (collision.gameObject.tag == "Turret")
        {
            if(collision.gameObject.GetComponent<FlameThrowerTurret>()!=null)
            {
                collision.gameObject.GetComponent<FlameThrowerTurret>().buffTurret();
            }
            if (collision.gameObject.GetComponent<TurretScr>() != null)
            {
                print("buffed turret");
                collision.gameObject.GetComponent<TurretScr>().buffTurret();
            }

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
