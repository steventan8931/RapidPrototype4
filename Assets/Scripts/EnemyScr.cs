using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScr : MonoBehaviour
{
    //basic stat
    public float maxHp = 100f;
    public float currentHp = 100f;

    public bool isDead = false;

    public float atkDmg = 20f;
    //move stat
    public float moveSpeed = 0.3f;
   //attack
    public float attackCD = 2.5f;
    public bool attacked;

    //state
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange, objInAttackRange;
    public GameObject buddy;
    public LayerMask playerMask;
    public LayerMask objMask;

    public Transform attackpoint;

    //animation
    public Animator EnemyAnimator;
    private void Awake()
    {
        buddy = GameObject.FindGameObjectWithTag("Buddy");
    }

    private void Update()
    {
        if (currentHp > 0)
        {
            playerInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, playerMask);
            objInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, objMask);

            if (objInAttackRange)
            {
                attackObj();

            }
            else if (playerInAttackRange)
            {
                attackPlayer();
            }
            else
            {
                EnemyAnimator.SetBool("IsAttacking", false);
                moveFunc();
            }
        }
    }

    public GameObject FindClosestWall(string Tag)
    {
        GameObject[] wallcollection;
        wallcollection = GameObject.FindGameObjectsWithTag(Tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in wallcollection)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public float checkDistPlayer()
    {
        float distance;
        distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        return distance;
    }
    
    public float checkDisBuddy()
    {
        float distance;
        distance = Vector3.Distance(transform.position, buddy.transform.position);
        return distance;
    }

    public float checkDisToWall()
    {
        float distance;
        distance = Vector3.Distance(transform.position, FindClosestWall("Wall").transform.position);
        return distance;
    }

    Vector3 moveTowards(GameObject target)
    {
       return Vector3.MoveTowards(transform.position,target.transform.position,moveSpeed);
 
    }

    void moveFunc()
    {
        if(FindClosestWall("Wall")!= null)
        {
            print("finded wall oof");
            float disToPlayer = checkDistPlayer();
            if(disToPlayer > 6f)
            {
                Vector3 tempTarget = FindClosestWall("Wall").transform.position;
                tempTarget.y = gameObject.transform.position.y;
                transform.LookAt(tempTarget);
                transform.position = moveTowards(FindClosestWall("Wall"));
            }else if(checkDisBuddy()<checkDisToWall())
            {
                Vector3 tempTarget = buddy.transform.position;
                tempTarget.y = gameObject.transform.position.y;
                transform.LookAt(tempTarget);
                transform.position = moveTowards(buddy);
            }
            else 
            {
                Vector3 tempTarget = GameObject.FindGameObjectWithTag("Player").transform.position;
                tempTarget.y = gameObject.transform.position.y;
                transform.LookAt(tempTarget);
                transform.position = moveTowards(GameObject.FindGameObjectWithTag("Player"));
            }


        }
        else
        {
            // move towards to buddy directly
            Vector3 tempTarget = buddy.transform.position;
            tempTarget.y = gameObject.transform.position.y;
            transform.LookAt(tempTarget);
            transform.position = moveTowards(buddy);
        }
        EnemyAnimator.SetBool("IsAttacking", false);
        EnemyAnimator.SetBool("IsWalking", true);
    }

    void attackObj()
    {
        //attack object,and the enemy won't move during attack
        
        EnemyAnimator.SetBool("IsWalking", false);
        
        if (!attacked)
        {
            Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position, attackRange, objMask);
            // Damage enemies
            foreach (Collider enemy in hitobjects)
            {
                if (enemy.GetComponent<Interactable>() != null)
                {
                    //damage them
                    enemy.GetComponent<Interactable>().TakeDamage((int)atkDmg);
                    Instantiate(enemy.GetComponent<Interactable>().m_ParticlePrefab, attackpoint.position, Quaternion.identity);
                }

                if (enemy.GetComponent<BuddyScr>() != null)
                {
                    //damage them

                    enemy.GetComponent<BuddyScr>().receiveDmg(atkDmg);
                }
                //debug message
                Debug.Log("enemy hit" + enemy.name);

            }

            //do damage
            print("attacking!");
            //add animation here
            EnemyAnimator.SetBool("IsAttacking", true);
            attacked = true;
            Invoke(nameof(resetAttack), attackCD);
        }
    }
    void attackPlayer()
    {
        EnemyAnimator.SetBool("IsWalking", false);
        if (!attacked)
        {
            Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position, attackRange, playerMask);
            // Damage enemies
            foreach (Collider enemy in hitobjects)
            {
                if (enemy.GetComponent<PlayerScr>() != null)
                {
                    //damage Player
                    enemy.GetComponent<PlayerScr>().receiveDmg((int)atkDmg);
                    EnemyAnimator.SetBool("IsAttacking", true);

                }
                //debug message
                Debug.Log("enemy hit" + enemy.name);
                
            }

            //do damage
            print("attacking!");
            //add animation here

            attacked = true;
            Invoke(nameof(resetAttack), attackCD);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }

    void resetAttack()
    {
        attacked = false;
    }

    public void receiveDmg(float dmg)
    {
        currentHp -= dmg;
        if(currentHp <=0)
        {
            currentHp = 0;
            isDead = true;
            //Play death animation
            EnemyAnimator.SetBool("IsWalking", false);
            EnemyAnimator.SetBool("Dying", true);
        }
    }
}
