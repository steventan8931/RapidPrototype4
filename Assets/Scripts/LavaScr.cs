using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScr : MonoBehaviour
{
    public float dmgVal = 0.05f;
    // Start is called before the first frame update
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
