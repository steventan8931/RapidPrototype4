using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public Transform attackpoint;
    public float attackrange = 0.6f;
    public LayerMask destroyableLayers;

    public float Maxhitpoints = 100f;
    public float currenthitpoints = 100f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // start melee attack animation

        // Detect enemies in range
        Collider[] hitobjects = Physics.OverlapSphere(attackpoint.position,attackrange,destroyableLayers);
        // Damage enemies
        foreach(Collider enemy in hitobjects)
        {
            //damage them

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
}
