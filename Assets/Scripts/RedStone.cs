using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStone : Interactable
{
    [Header("Redstone Spawner Components")]
    public GameObject m_RedstoneBlockPrefab;
    public Transform[] m_RedstoneSpawnPoints;

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
                Instantiate(m_RedstoneBlockPrefab, m_RedstoneSpawnPoints[i].position, Quaternion.identity);
            }
            m_AudioManager.PlaySound("RockBreak");
            Destroy(gameObject);
        }
    }
}
