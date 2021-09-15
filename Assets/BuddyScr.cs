using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyScr : MonoBehaviour
{
    public float CurrHp = 200;
    public float MaxHp = 200;
    public GameObject warningUi;
    // Start is called before the first frame update
    public void receiveDmg(float dmg)
    {
        if (CurrHp <= 0)
        {
            CurrHp = 0;
            // game over func
        }
        else
        {
            CurrHp -= dmg;
            //Show warning UI
            warningUi.SetActive(true);
            Invoke(nameof(disableWarning), 1.2f);
        }

    }

    public void disableWarning()
    {
        warningUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrHp <= 0)
        {
            warningUi.SetActive(false);
        }
    }
}
