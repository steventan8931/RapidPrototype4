using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSource : MonoBehaviour
{
    public float m_CurrentHP = 200.0f;
    public float m_MaxHP = 200.0f;

    public Image m_PowerSourceHPBar;

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

    void updateHpBar()
    {
        m_PowerSourceHPBar.fillAmount = (m_CurrentHP/m_MaxHP);
    }

    public void disableWarning()
    {
        //warningUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updateHpBar();
        if (m_CurrentHP <= 0)
        {
            //warningUi.SetActive(false);
        }
    }
}
