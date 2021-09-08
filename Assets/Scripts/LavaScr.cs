using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScr : MonoBehaviour
{
    public float dmgVal = 5f;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerScr>().receiveDmg(dmgVal / 2);
        }

        if(other.tag == "Enemy")
        {
           //do sustain damage
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
