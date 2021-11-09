using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject m_EnemyPrefab;
    public GameObject m_Enemy2Prefab;
    //public GameObject m_Enemy3Prefab;

    public int m_CrystalReward = 10;
    public float m_MoveSpeedBoost = 0.0f;
    public float m_HPBoost = 0.0f;

    public float m_SpawnDelay = 1.0f;
    public float m_SpawnTimer = 0.0f;

    public bool m_StartSpawning = false;
    public int m_EnemiesToSpawn = 5;
    public int m_EnemiesSpawned = 0;
    public int m_EnemiesRemain = 0;

    public int m_EnemiesSnakeToSpawn = 0;

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

                    GameObject temp;

                    //If there are enemy snakes to spawn
                    if (m_EnemiesSnakeToSpawn > 0)
                    {
                        //Spawn all the snakes first
                        temp = Instantiate(m_Enemy2Prefab, transform);
                    }
                    else //Spawn default enemy
                    {
                        temp = Instantiate(m_EnemyPrefab, transform);
                    }

                    temp.GetComponent<NewEnemyAI>().moveSpeed += m_MoveSpeedBoost;
                    temp.GetComponent<NewEnemyAI>().maxHp += m_HPBoost;
                    temp.GetComponent<NewEnemyAI>().currentHp += m_HPBoost;

                    m_EnemiesSnakeToSpawn--;    
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
