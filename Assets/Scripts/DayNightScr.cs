﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DayNightScr : MonoBehaviour
{
    public float timerDN = 150f;
    public Transform[] EnemyLoc;
    public Transform[] ResourceLoc;
    public GameObject enemyPrefab;
    public bool isNight = false;
    public bool isWin = false;

    //for enemy generation
    public bool isSpawning = false;
    public bool fullyspawned = false;
    public int enemyCount = 0;
    public float SpawningCd = 2.0f;
    public float currSpawnCd = 0f;

    public TextMeshProUGUI StageText, TimeText;
    public Animator BlackScreenCtrl;
    //for reminder
    public HudScr playerHud;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isWin)
        {
            DayNightCircle();
        }
        
    }
    void DayNightCircle()
    {
        timerDN -= Time.deltaTime;
        int tempTime = Mathf.FloorToInt(timerDN);
        TimeText.text =tempTime.ToString();
        if((timerDN >= 50f && timerDN <=51f) && isNight == false)
        {
            playerHud.showReminder(2);
        }
        
       
        if (timerDN <= 0 || isSpawning)
        {

            spawnEnemies();
            if (isNight == false)
            {
                timerDN = 150f;
                isNight = true;
                StageText.text = "Night Time";
                
                playerHud.showReminder(3);
            }
        }
        if(isNight == true && fullyspawned == true)
        {
            if(enemyCount <=3)
            {
                fullyspawned = false;
                spawnEnemies();
            }
        }
    }
    void spawnEnemies()
    {
        print("spawning enemies");
        if(fullyspawned)
        {
            return;
        }
        if(isSpawning == false)
        {
            isSpawning = true;
        }
        if(currSpawnCd <= 0 && fullyspawned == false)
        {
            foreach (Transform location in EnemyLoc)
            {
                float randnum = Random.Range(0, 2);
                if (randnum >= 1 && enemyCount < EnemyLoc.Length)
                {
                    Instantiate(enemyPrefab,location.position,Quaternion.identity);
                    enemyCount += 1;
                }
            }
            currSpawnCd = SpawningCd;
            if(enemyCount == EnemyLoc.Length)
            {
                fullyspawned = true;
                isSpawning = false;
            }
        }else
        {
            currSpawnCd -= Time.deltaTime;
        }
        
        
    }

    void checkWin()
    {
        if(timerDN <= 0 && isNight == true)
        {
            isWin = true;
            // Pop up win UI
            BlackScreenCtrl.SetBool("IsWin", true);
            // Swap Scene
            Invoke(nameof(loadWinScreen), 2.5f);
        }
    }

    void loadWinScreen()
    {
        SceneManager.LoadScene(2);
    }
}
