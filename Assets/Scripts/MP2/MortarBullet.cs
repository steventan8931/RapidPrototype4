using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBullet : MonoBehaviour
{
    private Transform target;
    public float lifetime = 0f;
    public float speed = 50f;
    public GameObject impactEffect;
    public float dmg = 25f;

    //Mortar bullet param
    public Rigidbody bulletBody;
    public float h = 25;
    public float gravity = -18;
    public float explodeRad = 10f;

    private void Awake()
    {
        bulletBody = gameObject.GetComponent<Rigidbody>();
    }
    private void Start()
    {
       
        
    }

    public void Seek(Transform tar)
    {
        Debug.Log("fired turret ball");
        target = tar;
        launch();
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Debug.Log("theres no target!");
            Destroy(gameObject);
            return;
        }
        destroyAfterWhile();
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

       // if (dir.magnitude <= distanceThisFrame)
        //{
         //   HitTarget();
          //  return;
        //}
       // transform.Translate(dir.normalized * distanceThisFrame, Space.World);
       // transform.LookAt(target);


    }

    private void HitTarget()
    {
        Debug.Log("hit something");
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Damage(target);
        Destroy(effectIns, 2f);
        Destroy(gameObject);
    }

    void destroyAfterWhile()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= 17f)
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
    void launch()
    {
        Physics.gravity = Vector3.up * gravity;
        bulletBody.useGravity = true;
        Debug.Log("enabled gravity");
        bulletBody.velocity = CalculateLaunchVelocity();
    }
    Vector3 CalculateLaunchVelocity()
    {
        float displacementY = target.position.y - bulletBody.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - bulletBody.position.x, 0, target.position.z - bulletBody.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        //time *= 2f;
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return velocityXZ + velocityY * -Mathf.Sign(gravity) ;
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRad);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
        //Add effect here
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
}
