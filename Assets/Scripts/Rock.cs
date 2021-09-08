using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Interactable
{
    [Header("Rock Spawner Components")]
    public GameObject m_RockBlockPrefab;
    public Transform[] m_RockSpawnPoints;

    private void Update()
    {
        if (m_Health <= 0)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            int spawnCount = (int)Random.Range(m_SpawnCountExtents.x, m_SpawnCountExtents.y);
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(m_RockBlockPrefab, m_RockSpawnPoints[i].position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
