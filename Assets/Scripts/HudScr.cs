using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScr : MonoBehaviour
{
    public GameObject meleeIcon, fenceIcon, wallIcon, bearIcon, LavaIcon;
    public LoadOut playerLoadout;
    public PlayerScr player;
    public Image playerHpBar;
    // Update is called once per frame
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScr>();
        playerLoadout = GameObject.FindGameObjectWithTag("Player").GetComponent<LoadOut>();
    }
    void Update()
    {
        changeLoadoutIcon();
        updatePlayerHp();
    }

    void changeLoadoutIcon()
    {
        if (playerLoadout.m_Hand == LoadOut.ActiveInHand.m_Melee)
        {
            meleeIcon.SetActive(true);
            fenceIcon.SetActive(false);
            wallIcon.SetActive(false);
            bearIcon.SetActive(false);
            LavaIcon.SetActive(false);
        }
        else if (playerLoadout.m_Hand == LoadOut.ActiveInHand.m_Fence)
        {
            meleeIcon.SetActive(false);
            fenceIcon.SetActive(true);
            wallIcon.SetActive(false);
            bearIcon.SetActive(false);
            LavaIcon.SetActive(false);
        }
        else if (playerLoadout.m_Hand == LoadOut.ActiveInHand.m_Wall)
        {
            meleeIcon.SetActive(false);
            fenceIcon.SetActive(false);
            wallIcon.SetActive(true);
            bearIcon.SetActive(false);
            LavaIcon.SetActive(false);
        }
        else if (playerLoadout.m_Hand == LoadOut.ActiveInHand.m_Trap)
        {
            meleeIcon.SetActive(false);
            fenceIcon.SetActive(false);
            wallIcon.SetActive(false);
            bearIcon.SetActive(true);
            LavaIcon.SetActive(false);
        }
    }

    void updatePlayerHp()
    {
        float percentage = player.currenthitpoints / 100;
        playerHpBar.fillAmount = percentage;
    }

        
        


}
