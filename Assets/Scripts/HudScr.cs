using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudScr : MonoBehaviour
{
    public GameObject meleeIcon, fenceIcon, wallIcon, bearIcon, LavaIcon;
    public LoadOut playerLoadout;
    public PlayerScr player;
    public Image playerHpBar;
    public float percentage;
    public TextMeshProUGUI reminder;
    // Update is called once per frame
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScr>();
        playerLoadout = GameObject.FindGameObjectWithTag("Player").GetComponent<LoadOut>();
    }
    void Update()
    {
        updatePlayerHp();
        changeLoadoutIcon();
        
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
        percentage = player.currenthitpoints / 100;
        print(percentage);
        playerHpBar.fillAmount = percentage;
    }

    public void showReminder(int index)
    {
        if(index == 1)
        {
            reminder.text = "Collect resources and build a defence to protect your buddy before dawn.";
        }
        else if(index == 2)
        {
            reminder.text = "Enemies are approaching soon, set up your defence.";
        }
        else if(index == 3)
        {
            reminder.text = "Enemies have spawned,protect ur buddy!";
        }


    }

        
        


}
