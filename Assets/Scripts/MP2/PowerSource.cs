using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSource : MonoBehaviour
{
    public float m_CurrentHP = 200.0f;
    public float m_MaxHP = 200.0f;

    public Image m_PowerSourceHPBar;

    public GameObject warningUi;
    public bool isShowingWarningUi = false;
    public float warningTimer = 0f;
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
            if(isShowingWarningUi == false)
            {
                isShowingWarningUi = true;
                warningUi.SetActive(true);
                warningUi.GetComponent<CanvasGroup>().alpha = 1;
            }
            warningTimer = 3f;
            //Show warning UI
            //warningUi.SetActive(true);
            //Invoke(nameof(disableWarning), 1.2f);
        }

    }
    public void decayOnWarning()
    {
        if(warningTimer >0)
        {
            warningTimer -= Time.deltaTime;
            if (warningTimer <= 0)
            {
                warningTimer = 0;
                //make warningUI disappear
                warningUi.GetComponent<Animator>().SetBool("isFading", true);
                Invoke(nameof(disableWarning), 1.1f);
            }
        }
        
    }
    void updateHpBar()
    {
        m_PowerSourceHPBar.fillAmount = (m_CurrentHP/m_MaxHP);
    }

    public void disableWarning()
    {
        warningUi.GetComponent<Animator>().SetBool("isFading", false);
        warningUi.GetComponent<CanvasGroup>().alpha = 1;
        isShowingWarningUi = false;
        warningUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updateHpBar();
        decayOnWarning();
        if (m_CurrentHP <= 0)
        {
            //warningUi.SetActive(false);
        }
    }
}
