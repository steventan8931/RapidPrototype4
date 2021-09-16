using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapScr : MonoBehaviour
{
    public int damageVal = 50;
    public LayerMask trappableMasks;
    public Animator trapAnim;
    public bool isActivated = false;
    Vector3 halfBox = new Vector3(0.8f, 0.3f, 0.8f);
    /*private void trapdetect()
    {
        Gizmos.DrawCube(transform.position, transform.localScale);
        Collider[] hitobjects = Physics.OverlapBox(transform.position,halfBox, Quaternion.identity,trappableMasks);
        foreach (Collider target in hitobjects)
        {
            //damage them
            
            //debug message
            Debug.Log("we hit" + target.name);

        }
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        /* if (collision.gameObject.tag == "Player")
         {
             print(collision.gameObject.tag);
             collision.gameObject.GetComponent<PlayerScr>().receiveDmg(damageVal / 2);
             Destroy(gameObject);
         }
        */
        if (isActivated == false)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                //enemy receive dmg function
                isActivated = true;
                trapAnim.SetBool("IsActive",true);
                collision.gameObject.GetComponent<EnemyScr>().receiveDmg(damageVal);
                Invoke(nameof(destroyTrap), 2f);
                
            }
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {

            other.gameObject.GetComponent<PlayerScr>().receiveDmg(damageVal / 2);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            //enemy receive dmg function
            other.gameObject.GetComponent<EnemyScr>().receiveDmg(damageVal);
            Destroy(gameObject);
        }
    }*/

    private void destroyTrap()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
