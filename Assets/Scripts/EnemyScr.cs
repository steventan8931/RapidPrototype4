using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScr : MonoBehaviour
{
    //basic stat
    public float maxHp = 100f;
    public float currentHp = 100f;

    public float atkDmg = 20f;
    //move stat
    public float moveSpeed = 0.3f;
   //attack
    public float attackCD = 1.5f;
    public bool attacked;

    //state
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange, objInAttackRange;

    public LayerMask playerMask;
    public LayerMask objMask;
    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        objInAttackRange = Physics.CheckSphere(transform.position, attackRange, objMask);
        
        if(objInAttackRange)
        {
            attackObj();
        }
        else
        {
           // moveFunc();
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

    Vector3 moveTowards(GameObject target)
    {
       return Vector3.MoveTowards(transform.position,target.transform.position,moveSpeed);
 
    }

    void moveFunc()
    {
        if(FindClosestWall("Wall")!= null)
        {
            print("finded wall oof");
             transform.position = moveTowards(FindClosestWall("Wall"));

        }
    }

    void attackObj()
    {
        //attack object,and the enemy won't move during attack
       // transform.LookAt(FindClosestWall("Wall").transform.position);
        if (!attacked)
        {
            //do damage
            print("attacking!");
            //add animation here

            attacked = true;
            Invoke(nameof(resetAttack), attackCD);
        }
    }

    void resetAttack()
    {
        attacked = false;
    }

    public void receiveDmg(float dmg)
    {
        currentHp -= dmg;
    }
}
