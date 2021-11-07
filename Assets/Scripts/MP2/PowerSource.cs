using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    public float m_CurrentHP = 200.0f;
    public float m_MaxHP = 200.0f;

    //public GameObject warningUi;
    // Start is called before the first frame update
    public void receiveDmg(float dmg)
    {
        if (m_CurrentHP <= 0)
        {
            m_CurrentHP = 0;
            // game over func
        }
        else
        {
            m_CurrentHP -= dmg;
            //Show warning UI
            //warningUi.SetActive(true);
            //Invoke(nameof(disableWarning), 1.2f);
        }

    }

    public void disableWarning()
    {
        //warningUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentHP <= 0)
        {
            //warningUi.SetActive(false);
        }
    }
}
