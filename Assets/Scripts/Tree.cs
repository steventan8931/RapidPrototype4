using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Interactable
{
    [Header("Tree Components")]
    public float m_DeathTimer = 0.0f;
    public float m_DespawnTimer = 7.0f;

    public float m_FallOverTime = 0.0f;
    public float m_FallSpeed = 1.0f;
    public Transform m_RotationPivot;

    [Header("Tree Spawner Components")]
    public GameObject m_WoodBlockPrefab;
    public Transform m_WoodBlockSpawnLocation;

    private void Update()
    {
        if (m_Health <= 0)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            m_FallOverTime += Time.deltaTime * m_FallSpeed;
            m_RotationPivot.rotation = Quaternion.Lerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f), m_FallOverTime);
            m_DeathTimer += Time.deltaTime;

            if (m_DeathTimer > m_DespawnTimer)
            {
                int spawnCount = (int)Random.Range(m_SpawnCountExtents.x, m_SpawnCountExtents.y);
                for (int i = 0; i <spawnCount; i++)
                {
                    Instantiate(m_WoodBlockPrefab, m_WoodBlockSpawnLocation.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                }
                Destroy(gameObject);
            }
        }
    }

}
