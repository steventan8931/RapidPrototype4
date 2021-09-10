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
    public float attackrange = 1.5f;
    public bool attacked = false;
    public float attackCD = 0.8f;
    public LayerMask destroyableLayers;

    public float Maxhitpoints = 100f;
    public float currenthitpoints = 100f;

    public bool isbuilder = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isbuilder == false && attacked == false) 
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
        if(attacked == true)
        {
            attackCD -= Time.deltaTime;
            if (attackCD <= 0)
            {
                attackCD = 0.8f;
                attacked = false;
            }
        }

        attackDetection();
       

    }

    void Attack()
    {
        
        print("playerattacking!");
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
            if(enemy.tag == "Enemy")
            {
                enemy.GetComponent<EnemyScr>().receiveDmg(10f);
            }

        }
        attacked = true;
    }

    void attackDetection()
    {
        Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position, attackrange, destroyableLayers);
        foreach (Collider enemy in hitobjects)
        {
            if(enemy != null)
            {
                print("detected destroyable obj");
            }
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
