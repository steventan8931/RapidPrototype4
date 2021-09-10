using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public Transform buildLoc;
    public GameObject buildobj;

    public Transform attackpoint;
    public float attackrange = 0.6f;
    public LayerMask destroyableLayers;

    public float Maxhitpoints = 100f;
    public float currenthitpoints = 100f;

    public bool isbuilder = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isbuilder == false) 
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && isbuilder == true)
        {
            buildInfront(buildobj);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            isbuilder = !isbuilder;
        }


    }

    void Attack()
    {
        // start melee attack animation

        // Detect enemies in range
        Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position,attackrange,destroyableLayers);
        // Damage enemies
        foreach (Collider enemy in hitobjects)
        {
            if (hitobjects[0].GetComponent<Interactable>() != null)
            {
                //damage them
                hitobjects[0].GetComponent<Interactable>().TakeDamage(1);
                Instantiate(hitobjects[0].GetComponent<Interactable>().m_ParticlePrefab, attackpoint.position, Quaternion.identity);
            }
            //debug message
            Debug.Log("we hit" + enemy.name);

        }
    }

    public void receiveDmg(float dmg)
    {
        currenthitpoints -= dmg;

    }

    private void OnDrawGizmosSelected()
    {
        if(attackpoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }

    void buildInfront(GameObject building)
    {
        Instantiate(building,buildLoc.position,Quaternion.identity);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.tag);
        if(collision.gameObject.tag == "BearTrap")
        {
            //receive damage
            currenthitpoints -= 25f;
            Destroy(collision.gameObject);
        }
    }*/
}
