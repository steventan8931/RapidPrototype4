using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyAnim : MonoBehaviour
{
    public Animator m_Animation;

    bool firstFrame = true;
    bool firstFrameRevive = true;

    AudioManager m_AudioManager;
    DayNightScr m_GameManager;

    public bool m_Saved = false;
    public GameObject m_ReviveParticle;
    public Transform m_ReviveTransform;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_GameManager = FindObjectOfType<DayNightScr>();
        m_Animation = GetComponent<Animator>();


        m_Animation.ResetTrigger("Dead");
        m_Animation.SetTrigger("Dead");
        m_Animation.SetBool("IsDead", true);
    }

    public void Revive()
    {

    }

    private void Update()
    {
        if (m_Saved)
        {
            if (firstFrameRevive)
            {
                m_AudioManager.PlaySound("PlayerRevive");
                m_Animation.SetBool("IsDead", false);
                m_Animation.ResetTrigger("Revive");
                m_Animation.SetTrigger("Revive");
                firstFrameRevive = false;
                Instantiate(m_ReviveParticle, m_ReviveTransform.position, Quaternion.identity);
            }
            m_GameManager.BuddyWin();
        }
        if (firstFrame)
        {
            m_AudioManager.PlaySound("PlayerHurt");
            firstFrame = false;
        }
    }

}
