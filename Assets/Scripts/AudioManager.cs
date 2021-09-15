using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip m_WoodSound, m_MetalSound, m_JumpSound, m_RockBreakSound, m_TreeFallSound, m_CraftSound, m_SwingSound, m_PickUpSound,
    m_SpawnItemSound, m_PlayerHurtSound, m_EnemyHurtSound, m_EnemyDeadSound;

    public AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string _Clip)
    {
        switch(_Clip)
        {
            case "Wood":
                    m_AudioSource.PlayOneShot(m_WoodSound);
                break;
            case "Metal":
                    m_AudioSource.PlayOneShot(m_MetalSound);
                break;
            case "Swing":
                    m_AudioSource.PlayOneShot(m_SwingSound);            
                break;
            case "Jump":
                    m_AudioSource.PlayOneShot(m_JumpSound);              
                break;
            case "TreeFall":
                if (!m_AudioSource.isPlaying)
                {
                    m_AudioSource.PlayOneShot(m_TreeFallSound);
                }
                break;
            case "RockBreak":
                if (!m_AudioSource.isPlaying)
                {
                    m_AudioSource.PlayOneShot(m_RockBreakSound);
                }
                break;
            case "Craft":
                    m_AudioSource.PlayOneShot(m_CraftSound);
                break;
            case "PickUp":
                m_AudioSource.PlayOneShot(m_PickUpSound);
                break;
            case "Spawn":
                m_AudioSource.PlayOneShot(m_SpawnItemSound);
                break;
            case "PlayerHurt":
                m_AudioSource.PlayOneShot(m_PlayerHurtSound);
                break;
            case "EnemyHurt":
                m_AudioSource.PlayOneShot(m_EnemyHurtSound);
                break;
            case "EnemyDead":
                m_AudioSource.PlayOneShot(m_EnemyDeadSound);
                break;
        }
    }
}
