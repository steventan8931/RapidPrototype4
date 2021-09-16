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
    public float attackDmg = 25f;
    public bool attacked = false;
    public float attackCD = 0.8f;
    public LayerMask destroyableLayers;

    public float Maxhitpoints = 100f;
    public float currenthitpoints = 100f;

    public bool isbuilder = false;

    public Animator m_Animation;

    public string m_BuildMaterial;

    Crafting m_Crafting;
    public bool m_IsCrafting = false;

    AudioManager m_AudioManager;

    public float m_DeathTimer = 0.0f;
    public float m_RespawnTime = 2.0f;
    public Vector3 m_CheckPoint;
    
    private void Start()
    {
        m_Crafting = FindObjectOfType<Crafting>();
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_CheckPoint = FindObjectOfType<BuddyAnim>().transform.position;
        m_CheckPoint += new Vector3(2.0f, 2.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsCrafting)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isbuilder == false && attacked == false)
            {
                m_AudioManager.PlaySound("Swing");
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && isbuilder == true)
            {
                buildInfront(buildobj);
            }
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            print("pressed B");
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

       //attackDetection();
       
        if (currenthitpoints <= 0)
        {
            m_Animation.SetBool("IsDead", true);
            Respawn();
        }
    }

    void Respawn()
    {
        if (m_DeathTimer == 0)
        {
            GetComponent<CharacterMotor>().enabled = false;
            m_Animation.ResetTrigger("Dead");
            m_Animation.SetTrigger("Dead");
        }

        m_DeathTimer += Time.deltaTime;
        
        if (m_DeathTimer > m_RespawnTime)
        {
            Debug.Log("dead");
            m_AudioManager.PlaySound("PlayerRevive");
            GetComponent<CharacterController>().enabled = false;
            transform.position = m_CheckPoint;
            m_Animation.SetBool("IsDead", false);
            currenthitpoints = Maxhitpoints;
            GetComponent<CharacterMotor>().enabled = true;
            GetComponent<CharacterController>().enabled = true;
            m_DeathTimer = 0;
        }

    }
    void Attack()
    {
        
        print("playerattacking!");
        // start melee attack animation
        m_Animation.ResetTrigger("Attacking");
        m_Animation.SetTrigger("Attacking");

        // Detect enemies in range
        Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position, attackrange, destroyableLayers);

        // Damage enemies
        foreach (Collider enemy in hitobjects)
        {   
            if (enemy.GetComponent<Tree>() != null)
            {
                //damage them
                m_AudioManager.PlaySound("Wood");
                enemy.GetComponent<Interactable>().TakeDamage(5);
                Instantiate(enemy.GetComponent<Interactable>().m_ParticlePrefab, attackpoint.position, Quaternion.identity);
            }
            if (enemy.GetComponent<Rock>() != null)
            {
                m_Animation.ResetTrigger("Mining");
                m_Animation.SetTrigger("Mining");
                m_AudioManager.PlaySound("Metal");
                //damage them
                enemy.GetComponent<Interactable>().TakeDamage(5);
                Instantiate(enemy.GetComponent<Interactable>().m_ParticlePrefab, attackpoint.position, Quaternion.identity);
            }
            if (enemy.GetComponent<RedStone>() != null)
            {
                m_Animation.ResetTrigger("Mining");
                m_Animation.SetTrigger("Mining");
                m_AudioManager.PlaySound("Metal");
                //damage them
                enemy.GetComponent<Interactable>().TakeDamage(5);
                Instantiate(enemy.GetComponent<Interactable>().m_ParticlePrefab, attackpoint.position, Quaternion.identity);
            }
            if (enemy.GetComponent<EnemyScr>() != null)
            {
                //damage them
                enemy.GetComponent<EnemyScr>().receiveDmg(attackDmg);
                //Instantiate(enemy.GetComponent<EnemyScr>().m_BloodFXPrefab, attackpoint.position, Quaternion.identity);
                Debug.Log("hitting");
            }
            //debug message
            Debug.Log("we hit" + enemy.name);
            //if (enemy.tag == "Enemy")
            //{
            //    enemy.GetComponent<EnemyScr>().receiveDmg(10f);
            //}

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
        if (dmg >= 5f)
        {
            m_AudioManager.PlaySound("PlayerHurt");
        }
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
        Instantiate(building, buildLoc.position, transform.rotation);
        m_Crafting.SetItemCostCount(1);
        m_Crafting.SetItemCost(m_BuildMaterial);
        m_AudioManager.PlaySound("Spawn");
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
