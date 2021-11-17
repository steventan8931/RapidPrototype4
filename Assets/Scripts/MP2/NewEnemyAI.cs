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
    public bool playerInSight, playerInAttackRange, objInAttackRange, PowerSourceInAttackRange;
    public GameObject m_AIStartPos;

    public LayerMask objMask;
    public LayerMask PowerSourceMask;

    public Transform attackpoint;

    public GameObject m_BloodFXPrefab;
    public GameObject m_DeathFXPrefab;

    //animation
    public Animator EnemyAnimator;

    //EnemySpawner
    bool cacheDeath = false;

    //Ice and Fire
    public bool onFire = false;
    public GameObject fireParticle;
    public bool onIce = false;
    float debuffTimer = 4f;
    float currDebuff = 0f;

    private MP2AudioManager m_AudioManager;

    private void Awake()
    {
        m_AIStartPos = GameObject.FindGameObjectWithTag("AIStart");
    }
    
    private void Start()
    {
        m_AudioManager = FindObjectOfType<MP2AudioManager>();
    }

    private void Update()
    {
        if (currentHp > 0)
        {
            //playerInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, playerMask);
            //objInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, objMask);
            calDebuff();
            PowerSourceInAttackRange = Physics.CheckSphere(attackpoint.position, attackRange, PowerSourceMask);
            if (objInAttackRange)
            {
                //attackObj();

            }
            else if (PowerSourceInAttackRange)
            {
                attackPowerSource();
                Debug.Log("attacking");

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
        else
        {
            if (!cacheDeath)
            {
                transform.parent.GetComponent<EnemySpawner>().m_EnemiesRemain--;
                cacheDeath = true;
            }
            if (maxHp > 600)
            {
                m_AudioManager.PlaySound("RockDeath");
            }
            else
            {
                m_AudioManager.PlaySound("FurDeath");
            }

           GameObject temp = Instantiate(m_DeathFXPrefab,transform);
            temp.transform.parent = null;
            Destroy(gameObject);
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
        distance = Vector3.Distance(transform.position, m_AIStartPos.transform.position);
        return distance;
    }

    Vector3 moveTowards(GameObject target)
    {
        if (onIce)
        {
            //If is snake
            if (maxHp > 600)
            {
                EnemyAnimator.speed = 0.2f;
            }
            else //For other enemy
            {
                EnemyAnimator.speed = 0.5f;
            }
            return Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * 0.35f * Time.deltaTime);
        }
        else
        {
            EnemyAnimator.speed = 1.0f;
            return Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }

    }

    void moveFunc()
    {
        // move towards to buddy directly
        Vector3 tempTarget = m_AIStartPos.transform.position;
        tempTarget.y = gameObject.transform.position.y;
        //transform.LookAt(tempTarget);

        Quaternion lookAtRot = Quaternion.LookRotation(m_AIStartPos.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRot, Time.deltaTime * 5f);

        transform.position = moveTowards(m_AIStartPos);
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


    void attackPowerSource()
    {
        EnemyAnimator.SetBool("IsWalking", false);
        if (!attacked)
        {
            Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position, attackRange, PowerSourceMask);
            // Damage enemies
            foreach (Collider enemy in hitobjects)
            {
                if (enemy.GetComponent<PowerSource>() != null)
                {
                    //damage Player
                    enemy.GetComponent<PowerSource>().receiveDmg(atkDmg);
                    EnemyAnimator.SetBool("IsAttacking", true);
                    //m_AudioManager.PlaySound("EnemyAttack");
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
        if (dmg >= 5 && isDead == false)
        {
            //m_AudioManager.PlaySound("EnemyHurt");
        }

        if (currentHp <= 0)
        {
            if (isDead == false)
            {
                currentHp = 0;
                isDead = true;
                //Play death animation
               // EnemyAnimator.SetBool("IsWalking", false);
                //EnemyAnimator.SetBool("Dying", true);
                //Invoke(nameof(destroywhendead), 5.5f);
                //m_AudioManager.PlaySound("EnemyDead");
            }
        }
    }
   public void caughtFire()
    {
        if(onIce == true)
        {
            onIce = false;
            currDebuff = 0;
            return;
        }else
        {
            onFire = true;
            fireParticle.SetActive(true);
            fireParticle.GetComponent<ParticleSystem>().Play();
            print("playing onfire effect");
            currDebuff = debuffTimer;
        }

    }
    public void caughtIce()
    {
        if (onFire == true)
        {
            onFire = false;
            fireParticle.SetActive(false);
            currDebuff = 0;
            return;
        }
        else
        {
            onIce = true;
            currDebuff = debuffTimer;
        }

    }
    void calDebuff()
    {
        if(onFire == true)
        {
            currentHp -= 10 * Time.deltaTime;
            currDebuff -= Time.deltaTime;
            if(currDebuff <= 0)
            {
                currDebuff = 0;
                fireParticle.SetActive(false);
                onFire = false;
            }
        }

        if (onIce == true)
        {
            currentHp -= 4 * Time.deltaTime;
            currDebuff -= Time.deltaTime;
            if (currDebuff <= 0)
            {
                currDebuff = 0;
                onIce = false;
            }
        }

    }
    //void destroywhendead()
    //{
    //    Instantiate(m_BloodBlockPrefab, transform.position, Quaternion.identity);
    //    Destroy(gameObject);
    //}
}
