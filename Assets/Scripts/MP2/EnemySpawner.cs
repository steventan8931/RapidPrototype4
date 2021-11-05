using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject m_EnemyPrefab;

    public float m_SpawnDelay = 1.0f;
    public float m_SpawnTimer = 0.0f;

    public bool m_StartSpawning = false;
    public int m_EnemiesToSpawn = 5;
    public int m_EnemiesSpawned = 0;

    public int m_EnemiesRemain = 0;

    private void Start()
    {
        //Rmove for actuals
        //m_StartSpawning = true;
        m_EnemiesRemain = m_EnemiesToSpawn;
    }

    private void Update()
    {
        if (m_StartSpawning)
        {
            if (m_EnemiesSpawned < m_EnemiesToSpawn)
            {
                m_SpawnTimer += Time.deltaTime;
                if (m_SpawnTimer > m_SpawnDelay)
                {
                    Debug.Log("spawned");
                    Instantiate(m_EnemyPrefab, transform);
                    m_EnemiesSpawned++;
                    m_SpawnTimer = 0.0f;
                }
            }  
            else
            {
                m_StartSpawning = false;
            }
        }
    }
}
