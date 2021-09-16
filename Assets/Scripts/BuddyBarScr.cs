using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuddyBarScr : MonoBehaviour
{
    public BuddyScr buddy;
    public Image buddyHpbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float percentage = buddy.CurrHp / buddy.MaxHp;
        buddyHpbar.fillAmount = percentage;
    }
}
