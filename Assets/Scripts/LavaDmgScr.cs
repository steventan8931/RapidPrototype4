using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDmgScr : MonoBehaviour
{
    public PlayerScr player;
    public float dmgVal = 2f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScr>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerScr>().receiveDmg(dmgVal);
        }

       /* if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyScr>().receiveDmg(dmgVal);
        }*/
    }


}
