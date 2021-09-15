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

    public TreePlayerPosCheck m_PosCheck;

    public Vector2 m_ScaleExtents = new Vector2(5.0f, 10.0f);
    public float cacheScale;
    public Vector2 m_RotationExtents = new Vector2(0.0f, 360.0f);
    public float cacheRotation;

    [Header("Tree Spawner Components")]
    public GameObject m_WoodBlockPrefab;
    public Transform m_WoodBlockSpawnLocation;

    AudioManager m_AudioManager;
    Vector3 cachePosCheck;
    bool cacheAudio = true;
    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        cacheScale = Random.Range(m_ScaleExtents.x, m_ScaleExtents.y);
        transform.localScale = new Vector3(cacheScale, cacheScale, cacheScale);
        cacheRotation = Random.Range(m_RotationExtents.x, m_RotationExtents.y);
        transform.localRotation = Quaternion.Euler(0.0f, cacheRotation, 0.0f);

        if (cacheScale > 7.5f)
        {
            m_Health = 20;
        }
        else if (cacheScale < 5f)
        {
            m_Health = 5;
        }
    }

    private void Update()
    {

        if (m_Health <= 0)
        {
            if(cacheAudio)
            {
                m_AudioManager.PlaySound("TreeFall");
                Debug.Log("play audio");
                cacheAudio = false;
            }

            gameObject.GetComponent<Collider>().enabled = false;
            m_FallOverTime += Time.deltaTime * m_FallSpeed;

            if (m_FallOverTime > 0.6f)
            {

                m_FallOverTime += Time.deltaTime * m_FallSpeed;
            }
            if (transform.position.x - cachePosCheck.x > 0)
            {
                m_RotationPivot.rotation = Quaternion.Lerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, -90.0f), m_FallOverTime);
            }
            else
            {
                m_RotationPivot.rotation = Quaternion.Lerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f), m_FallOverTime);
            }

            m_DeathTimer += Time.deltaTime;

            if (m_DeathTimer > m_DespawnTimer)
            {
                int spawnCount = (int)Random.Range(m_SpawnCountExtents.x, m_SpawnCountExtents.y + cacheScale);
                for (int i = 0; i < spawnCount; i++)
                {
                    Instantiate(m_WoodBlockPrefab, m_WoodBlockSpawnLocation.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                }
                Destroy(gameObject);
            }
        }
        else
        {
            cachePosCheck = m_PosCheck.m_PlayerPos;
        }

    }

}
