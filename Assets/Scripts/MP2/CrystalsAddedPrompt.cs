using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalsAddedPrompt : MonoBehaviour
{
    public RectTransform m_OnScreenPos;
    public RectTransform m_OffScreenPos;

    public RectTransform m_CrystalsAddedRect;
    public Text m_CrystalAddedCount;

    public bool m_OnScreen = false;
    public float m_TransitionSpeed = 0.5f;
    public float m_FadeSpeed = 100.0f;
    public float m_FadeTimer = 0.0f;
    private WavesManager m_WaveManager;
    private NewInventory m_Inventory;
    private int cacheCrystalCount;
    private Color32 cacheTextColor;
    private void Start()
    {
        m_WaveManager = FindObjectOfType<WavesManager>();
        m_Inventory = FindObjectOfType<NewInventory>();
        cacheTextColor = m_CrystalAddedCount.color;
    }

    private void Update()
    {
        if (m_WaveManager.m_WaveIsActive)
        {
            cacheCrystalCount = m_Inventory.m_MagicCrystalCount;
        }

        m_CrystalAddedCount.text = m_WaveManager.m_CurrentSpawner.m_CrystalReward.ToString();
        
        if (cacheCrystalCount != m_Inventory.m_MagicCrystalCount)
        {
            m_OnScreen = true;
        }
        else
        {
            m_OnScreen = false;
        }
            
        if (m_OnScreen)
        {
            //Set it on the screen
            m_CrystalsAddedRect.position = m_OnScreenPos.position;

            //Reset Variables when it left screen
            m_FadeTimer = 0.0f;
            m_CrystalAddedCount.color = cacheTextColor;
            m_CrystalsAddedRect.GetComponent<Text>().color = cacheTextColor; 

            //Move it out of the screen
            cacheCrystalCount = m_Inventory.m_MagicCrystalCount;
        }
        else
        {
            //Move up and out of screen
            m_CrystalsAddedRect.position = Vector3.Lerp(m_CrystalsAddedRect.position, m_OffScreenPos.position, Time.deltaTime * m_TransitionSpeed);

            //Fade out
            m_FadeTimer -= Time.deltaTime * m_FadeSpeed;
            m_CrystalAddedCount.color = new Color32(cacheTextColor.r, cacheTextColor.g, cacheTextColor.b, (byte)m_FadeTimer);
            m_CrystalsAddedRect.GetComponent<Text>().color = new Color32(cacheTextColor.r, cacheTextColor.g, cacheTextColor.b, (byte)m_FadeTimer);
        }
    }
}
