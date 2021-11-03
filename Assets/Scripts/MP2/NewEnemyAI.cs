using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyAI : MonoBehaviour
{
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
    }

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (currentHp > 0)
        {
            //playerInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, playerMask);
            //objInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, objMask);
            //buddyInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, buddyMask);
            if (objInAttackRange)
            {
                //attackObj();

            }
            else if (buddyInAttackRange)
            {
                //attackBuddy();

            }
            else if (playerInAttackRange)
            {
                //attackPlayer();
            }
            else
            {
                //EnemyAnimator.SetBool("IsAttacking", false);
                moveFunc();
            }
        }
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

    Vector3 moveTowards(GameObject target)
    {        
        return Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

    }

    void moveFunc()
    {
        // move towards to buddy directly
        Vector3 tempTarget = buddy.transform.position;
        tempTarget.y = gameObject.transform.position.y;
        transform.LookAt(tempTarget);
        transform.position = moveTowards(buddy);
        transform.position = new Vector3(transform.position.x, tempTarget.y, transform.position.z);
        //EnemyAnimator.SetBool("IsAttacking", false);
        //EnemyAnimator.SetBool("IsWalking", true);
    }


    //void attackObj()
    //{
    //    //attack object,and the enemy won't move during attack

    //    EnemyAnimator.SetBool("IsWalking", false);

    //    if (!attacked)
    //    {
    //        Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position, attackRange, objMask);
    //        // Damage enemies
    //        foreach (Collider enemy in hitobjects)
    //        {
    //            if (enemy.GetComponent<Interactable>() != null)
    //            {
    //                //damage them
    //                enemy.GetComponent<Interactable>().TakeDamage((int)atkDmg);
    //                Instantiate(enemy.GetComponent<Interactable>().m_ParticlePrefab, attackpoint.position, Quaternion.identity);
    //                m_AudioManager.PlaySound("EnemyAttack");
    //            }

    //            if (enemy.GetComponent<BuddyScr>() != null)
    //            {
    //                //damage them
    //                Instantiate(m_BloodFXPrefab, attackpoint.position, Quaternion.identity);
    //                enemy.GetComponent<BuddyScr>().receiveDmg(atkDmg);
    //                m_AudioManager.PlaySound("EnemyAttack");
    //            }
    //            //debug message
    //            Debug.Log("enemy hit" + enemy.name);

    //        }

    //        //do damage
    //        print("attacking!");
    //        //add animation here
    //        EnemyAnimator.SetBool("IsAttacking", true);
    //        attacked = true;
    //        Invoke(nameof(resetAttack), attackCD);
    //    }
    //}
    //void attackPlayer()
    //{
    //    EnemyAnimator.SetBool("IsWalking", false);
    //    if (!attacked)
    //    {
    //        Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position, attackRange, playerMask);
    //        // Damage enemies
    //        foreach (Collider enemy in hitobjects)
    //        {
    //            if (enemy.GetComponent<PlayerScr>() != null)
    //            {
    //                //damage Player
    //                enemy.GetComponent<PlayerScr>().receiveDmg((int)atkDmg);
    //                EnemyAnimator.SetBool("IsAttacking", true);
    //                Instantiate(m_BloodFXPrefab, attackpoint.position, Quaternion.identity);
    //                m_AudioManager.PlaySound("EnemyAttack");
    //            }
    //            //debug message
    //            Debug.Log("enemy hit" + enemy.name);

    //        }

    //        //do damage
    //        print("attacking!");
    //        //add animation here

    //        attacked = true;
    //        Invoke(nameof(resetAttack), attackCD);
    //    }
    //}

    //void attackBuddy()
    //{
    //    EnemyAnimator.SetBool("IsWalking", false);
    //    if (!attacked)
    //    {
    //        Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position, attackRange, buddyMask);
    //        // Damage enemies
    //        foreach (Collider enemy in hitobjects)
    //        {
    //            if (enemy.GetComponent<BuddyScr>() != null)
    //            {
    //                //damage Player
    //                enemy.GetComponent<BuddyScr>().receiveDmg(atkDmg);
    //                EnemyAnimator.SetBool("IsAttacking", true);
    //                m_AudioManager.PlaySound("EnemyAttack");
    //            }
    //            //debug message
    //            Debug.Log("enemy hit" + enemy.name);

    //        }

    //        //do damage
    //        print("attacking!");
    //        //add animation here

    //        attacked = true;
    //        Invoke(nameof(resetAttack), attackCD);
    //    }
    //}
    //private void OnDrawGizmosSelected()
    //{
    //    if (attackpoint == null)
    //    {
    //        return;
    //    }

    //    Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    //}

    //void resetAttack()
    //{
    //    attacked = false;
    //}

    //public void receiveDmg(float dmg)
    //{
    //    currentHp -= dmg;
    //    Instantiate(m_BloodFXPrefab, attackpoint.position, Quaternion.identity);
    //    if (dmg >= 5 && isDead == false)
    //    {
    //        m_AudioManager.PlaySound("EnemyHurt");
    //    }

    //    if (currentHp <= 0)
    //    {
    //        if (isDead == false)
    //        {
    //            currentHp = 0;
    //            isDead = true;
    //            //Play death animation
    //            EnemyAnimator.SetBool("IsWalking", false);
    //            EnemyAnimator.SetBool("Dying", true);
    //            Invoke(nameof(destroywhendead), 5.5f);
    //            m_AudioManager.PlaySound("EnemyDead");
    //        }
    //    }
    //}

    //void destroywhendead()
    //{
    //    Instantiate(m_BloodBlockPrefab, transform.position, Quaternion.identity);
    //    Destroy(gameObject);
    //}
}
