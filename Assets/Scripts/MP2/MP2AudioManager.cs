using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2AudioManager : MonoBehaviour
{
    public AudioClip m_TeleportSound, m_FireSpellSound, m_IceSpellSound, m_BasicSpellSound, m_RockEnemyDeath, m_FurEnemyDeath;

    public AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string _Clip)
    {
        switch (_Clip)
        {
            case "Teleport":
                m_AudioSource.volume = 0.5f;
                m_AudioSource.PlayOneShot(m_TeleportSound);
                break;
            case "Fire":
                m_AudioSource.volume = 0.3f;
                m_AudioSource.PlayOneShot(m_FireSpellSound);
                break;
            case "Ice":
                m_AudioSource.volume = 0.3f;
                m_AudioSource.PlayOneShot(m_IceSpellSound);
                break;
            case "Basic":
                m_AudioSource.volume = 1.0f;
                m_AudioSource.PlayOneShot(m_BasicSpellSound);
                break;
            case "RockDeath":
                m_AudioSource.volume = 0.5f;
                m_AudioSource.PlayOneShot(m_RockEnemyDeath);
                break;
            case "FurDeath":
                m_AudioSource.volume = 0.3f;
                m_AudioSource.PlayOneShot(m_FurEnemyDeath);
                break;
        }
    }
}
