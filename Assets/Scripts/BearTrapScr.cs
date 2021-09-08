using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapScr : MonoBehaviour
{
    public int damageVal = 50;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerScr>().receiveDmg(damageVal/2);
        }

        if(other.tag == "Enemy")
        {
            //enemy receive dmg function
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
