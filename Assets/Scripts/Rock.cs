using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Interactable
{
    [Header("Rock Spawner Components")]
    public GameObject m_RockBlockPrefab;
    public Transform[] m_RockSpawnPoints;

    AudioManager m_AudioManager;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
    }


    private void Update()
    {
        if (m_Health <= 0)
        {
            m_AudioManager.m_AudioSource.Stop();
            gameObject.GetComponent<Collider>().enabled = false;
            int spawnCount = (int)Random.Range(m_SpawnCountExtents.x, m_SpawnCountExtents.y);
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(m_RockBlockPrefab, m_RockSpawnPoints[i].position, Quaternion.identity);
            }
            m_AudioManager.PlaySound("RockBreak");
            Destroy(gameObject);
        }
    }
}
