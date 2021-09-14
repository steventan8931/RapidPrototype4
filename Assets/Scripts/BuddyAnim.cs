using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyAnim : MonoBehaviour
{
    public Animator m_Animation;

    bool firstFrame = true;

    AudioManager m_AudioManager;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_Animation = GetComponent<Animator>();


        m_Animation.ResetTrigger("Dead");
        m_Animation.SetTrigger("Dead");
        m_Animation.SetBool("IsDead", true);
    }

    private void Update()
    {
        if (firstFrame)
        {
            m_AudioManager.PlaySound("PlayerHurt");
            firstFrame = false;
        }
    }

}
