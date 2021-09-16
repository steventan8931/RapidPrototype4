using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudScr : MonoBehaviour
{
    public GameObject meleeIcon, fenceIcon, wallIcon, bearIcon, LavaIcon, DecoyIcon;
    public LoadOut playerLoadout;
    public PlayerScr player;
    public Image playerHpBar;
    public float percentage;
    //public TextMeshProUGUI reminder;

    public GameObject m_Reminder1;
    public GameObject m_Reminder2;
    public GameObject m_Reminder3;
    public float m_ReminderTimer = 0.0f;
    public float m_ReminderTimeout = 3.0f;
    public float m_DecayTimer = 255;

    // Update is called once per frame
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScr>();
        playerLoadout = GameObject.FindGameObjectWithTag("Player").GetComponent<LoadOut>();
    }

    public void Prompt(GameObject _Reminder)
    {
        if (_Reminder.activeInHierarchy)
        {
            //m_ReminderTimer += Time.deltaTime;
            m_DecayTimer -= Time.deltaTime * 40;
            _Reminder.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)m_DecayTimer);
            if (_Reminder.GetComponent<Image>().color.a <= 0)
            {
                m_ReminderTimer = 0.0f;
                m_DecayTimer = 255;
                _Reminder.SetActive(false);
            }
        }
    }
    void Update()
    {
        updatePlayerHp();
        changeLoadoutIcon();

        Prompt(m_Reminder1);
        Prompt(m_Reminder2);
        Prompt(m_Reminder3);
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
            DecoyIcon.SetActive(false);
        }
        else if (playerLoadout.m_Hand == LoadOut.ActiveInHand.m_Fence)
        {
            meleeIcon.SetActive(false);
            fenceIcon.SetActive(true);
            wallIcon.SetActive(false);
            bearIcon.SetActive(false);
            LavaIcon.SetActive(false);
            DecoyIcon.SetActive(false);
        }
        else if (playerLoadout.m_Hand == LoadOut.ActiveInHand.m_Wall)
        {
            meleeIcon.SetActive(false);
            fenceIcon.SetActive(false);
            wallIcon.SetActive(true);
            bearIcon.SetActive(false);
            LavaIcon.SetActive(false);
            DecoyIcon.SetActive(false);
        }
        else if (playerLoadout.m_Hand == LoadOut.ActiveInHand.m_Decoy)
        {
            meleeIcon.SetActive(false);
            fenceIcon.SetActive(false);
            wallIcon.SetActive(true);
            bearIcon.SetActive(false);
            LavaIcon.SetActive(false);
            DecoyIcon.SetActive(true);
        }
        else if (playerLoadout.m_Hand == LoadOut.ActiveInHand.m_LavaTrap)
        {
            meleeIcon.SetActive(false);
            fenceIcon.SetActive(false);
            wallIcon.SetActive(false);
            bearIcon.SetActive(false);
            LavaIcon.SetActive(true);
            DecoyIcon.SetActive(false);
        }
        else if (playerLoadout.m_Hand == LoadOut.ActiveInHand.m_Trap)
        {
            meleeIcon.SetActive(false);
            fenceIcon.SetActive(false);
            wallIcon.SetActive(false);
            bearIcon.SetActive(true);
            LavaIcon.SetActive(false);
            DecoyIcon.SetActive(false);
        }
    }

    void updatePlayerHp()
    {
        percentage = player.currenthitpoints / 100;
        //print(percentage);
        playerHpBar.fillAmount = percentage;
    }

    public void showReminder(int index)
    {
        if (index == 1)
        {
            //reminder.text = "Collect resources and build a defence to protect your buddy before dawn.";
            m_Reminder1.SetActive(true);
        }
        else if(index == 2)
        {
            //reminder.text = "Enemies are approaching soon, set up your defence.";
            m_Reminder2.SetActive(true);
        }
        else if(index == 3)
        {
            //reminder.text = "Enemies have spawned,protect ur buddy!";
            m_Reminder3.SetActive(true);
        }


    }

        
        


}
