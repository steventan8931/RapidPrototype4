﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSource : MonoBehaviour
{
    public float m_CurrentHP = 200.0f;
    public float m_MaxHP = 200.0f;

    public Image m_PowerSourceHPBar;

    public GameObject warningUi;
    public GameObject failUi;
    private RestrictControl restrictCtrl;
    private ScreenShakeScr screenshake;
    public bool isShowingWarningUi = false;
    public float warningTimer = 0f;
    private void Awake()
    {
        restrictCtrl = FindObjectOfType<RestrictControl>();
        screenshake = FindObjectOfType<ScreenShakeScr>();
    }
    public void receiveDmg(float dmg)
    {
        if (m_CurrentHP <= 0)
        {
            m_CurrentHP = 0;
            // game over func
            failFunc();
        }
        else
        {
            m_CurrentHP -= dmg;
            
            screenshake.StartCoroutine(screenshake.ShakeScreen());
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
    public void failFunc()
    {
        warningUi.SetActive(false);
        failUi.SetActive(true);
        restrictCtrl.DisableControls();
        
    }
    // Update is called once per frame
    void Update()
    {
        updateHpBar();
        decayOnWarning();
        
    }
}
