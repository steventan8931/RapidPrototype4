using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesManager : MonoBehaviour
{
    [Header("Per Wave Components")]
    public List<EnemySpawner> m_EnemySpawners;
    public EnemySpawner m_CurrentSpawner;
    private bool cacheFirstWave = true;
    public bool m_WaveIsActive = false;

    [Header("UI + Win Components")]
    public float m_NextWaveTimer = 0.0f;
    public float m_NextWaveDelay = 5.0f;
    public GameObject m_NextWaveUIPrompt;
    public Text m_NextWaveComingInCount;
    public GameObject m_WinPrompt;

    public int m_WaveCount = 1;
    public Text m_WaveText;

    public bool m_GameWin = false;
    public bool m_LastRound = false;


    private NewInventory m_PlayerIventory;
    private CrystalsAddedPrompt m_Prompt;

    private void Start()
    {
        m_PlayerIventory = FindObjectOfType<NewInventory>();
        m_Prompt = FindObjectOfType<CrystalsAddedPrompt>();
        //If there are no preset spawners
        if (m_EnemySpawners.Count <= 0)
        {
            //Change to preset objects later
            EnemySpawner[] tempList = FindObjectsOfType<EnemySpawner>();
            for (int i = 0; i < tempList.Length; i++)
            {
                m_EnemySpawners.Add(tempList[i]);
            }
        }

        m_CurrentSpawner = m_EnemySpawners[0];

        //Update Current Wave Number
        m_WaveText.text = m_WaveCount.ToString();
    }

    private void Update()
    {
        if (m_GameWin)
        {
            m_WinPrompt.SetActive(true);
            m_NextWaveUIPrompt.SetActive(false);
            return;
        }

        if (m_EnemySpawners.Count >= 1)
        {
            if (m_EnemySpawners[0].m_StartSpawning)
            {
                m_CurrentSpawner = m_EnemySpawners[0];
                m_WaveIsActive = true;
            }
        }    


        //After it has finished spawning
        if (!m_CurrentSpawner.m_StartSpawning && m_WaveIsActive)
        {
            if (m_CurrentSpawner.m_EnemiesRemain <= 0)
            {
                //Add the crystals to player inventory after the round
                m_PlayerIventory.m_MagicCrystalCount += m_CurrentSpawner.m_CrystalReward;
                m_Prompt.m_OnScreen = true;
                m_WaveIsActive = false;
            }
        }

        //If there is no current wave
        if (!m_WaveIsActive)
        {
            //Press Enter to start next round earlier
            if (Input.GetKeyDown(KeyCode.Return))
            {
                m_NextWaveTimer = m_NextWaveDelay;
            }

            m_NextWaveTimer += Time.deltaTime;
            if (!m_LastRound)
            {
                m_NextWaveUIPrompt.SetActive(true);
                m_NextWaveComingInCount.text = ((int)m_NextWaveDelay - (int)m_NextWaveTimer).ToString();
            }
            else
            {
                m_NextWaveTimer = m_NextWaveDelay;
            }
            //After delay start next wave
            if (m_NextWaveTimer >= m_NextWaveDelay)
            {
                if (m_EnemySpawners.Count >= 1)
                {
                    if (!cacheFirstWave)
                    {
                        //Remove the last enemy spawner from the list
                        m_EnemySpawners.RemoveAt(0);

                        if (m_EnemySpawners.Count <= 1)
                        {
                            m_LastRound = true;
                        }
                        //Add wave Count
                        m_WaveCount++;
                    }
                    cacheFirstWave = false;
                    if (m_EnemySpawners.Count >= 1)
                    {
                        //Start the spawning for the next enemy spawner
                        m_EnemySpawners[0].m_StartSpawning = true;
                    }
                    //Reset Timer 
                    m_NextWaveTimer = 0.0f;

                    //Update Current Wave Number
                    m_WaveText.text = m_WaveCount.ToString();

                    //Set the wave to being active
                    m_WaveIsActive = true;
                }
                else
                {
                    m_GameWin = true;
                }
            }
        }
        else
        {
            m_NextWaveUIPrompt.SetActive(false);
            //Press Enter to start next round earlier
            if (Input.GetKeyDown(KeyCode.Return))
            {
                for (int i = 0; i < m_EnemySpawners[0].transform.childCount; i++)
                {
                    m_EnemySpawners[0].transform.GetChild(i).GetComponent<NewEnemyAI>().moveSpeed += 10;
                }
            }
        }
    }
}
