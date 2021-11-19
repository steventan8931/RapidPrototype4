using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject m_EnemyT1Prefab;
    public GameObject m_EnemyT2Prefab;
    public GameObject m_EnemyT3Prefab;
    public GameObject m_EnemyT4Prefab;

    public int m_CrystalReward = 10;
    public float m_MoveSpeedBoost = 0.0f;
    public float m_HPBoost = 0.0f;

    public float m_SpawnDelay = 1.0f;
    public float m_SpawnTimer = 0.0f;

    public bool m_StartSpawning = false;
    public int m_TotalEnemiesToSpawn = 5;
    public int m_EnemiesSpawned = 0;
    public int m_EnemiesRemain = 0;

    public int m_EnemiesT2ToSpawn = 0;
    public int m_EnemiesT3ToSpawn = 0;
    public int m_EnemiesT4ToSpawn = 0;

    public GameObject m_SpawnFX;
    public GameObject m_SpawnFXPos;
    private void Start()
    {
        //Rmove for actuals
        //m_StartSpawning = true;
        m_EnemiesRemain = m_TotalEnemiesToSpawn;
    }

    private void Update()
    {
        if (m_StartSpawning)
        {
            if (m_EnemiesSpawned < m_TotalEnemiesToSpawn)
            {
                m_SpawnTimer += Time.deltaTime;
                if (m_SpawnTimer > m_SpawnDelay)
                {
                    Debug.Log("spawned");

                    GameObject temp;
                    Instantiate(m_SpawnFX, m_SpawnFXPos.transform);
                    //Spawns stronger enemies before weaker ones
                    //If there are T4 enemies to spawn
                    if (m_EnemiesT4ToSpawn > 0)
                    {
                        //Spawn all the T4 enemies first
                        temp = Instantiate(m_EnemyT4Prefab, transform);
                        m_EnemiesT4ToSpawn--;
                    }
                    else if (m_EnemiesT3ToSpawn > 0)   //If there are T3 enemies to spawn
                    {
                        //Spawn all the T3 enemies first
                        temp = Instantiate(m_EnemyT3Prefab, transform);
                        m_EnemiesT3ToSpawn--;
                    }
                    else if (m_EnemiesT2ToSpawn > 0)  //If there are T2 enemies to spawn
                    {
                        //Spawn all the 2 enemies first
                        temp = Instantiate(m_EnemyT2Prefab, transform);
                        m_EnemiesT2ToSpawn--;
                    }
                    else //Spawn default enemy
                    {
                        temp = Instantiate(m_EnemyT1Prefab, transform);
                    }

                    temp.GetComponent<NewEnemyAI>().moveSpeed += m_MoveSpeedBoost;
                    temp.GetComponent<NewEnemyAI>().maxHp += m_HPBoost;
                    temp.GetComponent<NewEnemyAI>().currentHp += m_HPBoost;

   
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
