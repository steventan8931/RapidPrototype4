using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScr : MonoBehaviour
{
    BoxCollider selfBoxCollider;
    public Rigidbody TrapRigibody;
    public float dmgVal = 0.05f;
    Vector3 tempLoc;
    // Start is called before the first frame update
    private void Awake()
    {
        selfBoxCollider = gameObject.GetComponent<BoxCollider>();
        //tempLoc = gameObject.transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        tempLoc = gameObject.transform.position;
        if(collision.gameObject.tag == "Floor")
        {
            selfBoxCollider.isTrigger = true;
            Destroy(TrapRigibody);
            print("destroied rigi body");
            transform.position = tempLoc;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerScr>().receiveDmg(dmgVal / 2);
        }

        if(other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyScr>().receiveDmg(dmgVal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
