using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScr : MonoBehaviour
{
    public DayNightScr gameManager;
    //basic stat
    public float maxHp = 100f;
    public float currentHp = 100f;

    public bool isDead = false;

    public float atkDmg = 10f;
    //move stat
    public float moveSpeed = 0.3f;
   //attack
    public float attackCD = 2.5f;
    public bool attacked;

    //state
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange, objInAttackRange, buddyInAttackRange;
    public GameObject buddy;
    public LayerMask playerMask;
    public LayerMask objMask;
    public LayerMask buddyMask;

    public Transform attackpoint;

    AudioManager m_AudioManager;
    public GameObject m_BloodFXPrefab;
    public GameObject m_BloodBlockPrefab;

    //animation
    public Animator EnemyAnimator;
    private void Awake()
    {
        buddy = GameObject.FindGameObjectWithTag("Buddy");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DayNightScr>();
    }

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
    }

        private void Update()
    {
        if (currentHp > 0)
        {
            playerInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, playerMask);
            objInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, objMask);
            buddyInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, buddyMask);
            if(objInAttackRange)
            {
                attackObj();
               
            }
            else if (buddyInAttackRange)
            {
                attackBuddy();

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
       if(FindClosestWall("Decoy")!= null)
        {
            print("finded decoy oof");
            float disToPlayer = checkDistPlayer();
            if(disToPlayer > 5f)
            {
                Vector3 tempTarget = FindClosestWall("Decoy").transform.position;
                tempTarget.y = gameObject.transform.position.y;
                transform.LookAt(tempTarget);
                transform.position = moveTowards(FindClosestWall("Decoy"));
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
                    m_AudioManager.PlaySound("EnemyAttack");
                }

                if (enemy.GetComponent<BuddyScr>() != null)
                {
                    //damage them
                    Instantiate(m_BloodFXPrefab, attackpoint.position, Quaternion.identity);
                    enemy.GetComponent<BuddyScr>().receiveDmg(atkDmg);
                    m_AudioManager.PlaySound("EnemyAttack");
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
                    Instantiate(m_BloodFXPrefab, attackpoint.position, Quaternion.identity);
                    m_AudioManager.PlaySound("EnemyAttack");
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

    void attackBuddy()
    {
        EnemyAnimator.SetBool("IsWalking", false);
        if (!attacked)
        {
            Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position, attackRange, buddyMask);
            // Damage enemies
            foreach (Collider enemy in hitobjects)
            {
                if (enemy.GetComponent<BuddyScr>() != null)
                {
                    //damage Player
                    enemy.GetComponent<BuddyScr>().receiveDmg(atkDmg);
                    EnemyAnimator.SetBool("IsAttacking", true);
                    m_AudioManager.PlaySound("EnemyAttack");
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
        Instantiate(m_BloodFXPrefab, attackpoint.position, Quaternion.identity);
        m_AudioManager.PlaySound("EnemyHurt");
        
        if (currentHp <=0)
        {
            if (isDead == false)
            {
                currentHp = 0;
                isDead = true;
                //Play death animation
                EnemyAnimator.SetBool("IsWalking", false);
                EnemyAnimator.SetBool("Dying", true);
                gameManager.enemyCount -= 1;
                Invoke(nameof(destroywhendead), 5.5f);
                m_AudioManager.PlaySound("EnemyDead");
            }
        }
    }

    void destroywhendead()
    {
        Instantiate(m_BloodBlockPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
