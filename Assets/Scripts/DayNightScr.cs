﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScr : MonoBehaviour
{
    public float timerDN = 0f;
    public Transform[] EnemyLoc;
    public Transform[] ResourceLoc;
    public GameObject enemyPrefab;
    public bool isNight = false;

    //for enemy generation
    public bool isSpawning = false;
    public bool fullyspawned = false;
    public int enemyCount = 0;
    public float SpawningCd = 2.0f;
    public float currSpawnCd = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerDN -= Time.deltaTime;
        if(timerDN <= 0 || isSpawning)
        {
            spawnEnemies();
            if (isNight == false)
            {
                isNight = true;
                timerDN = 300f;
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
        if(currSpawnCd <= 0)
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
            }
        }else
        {
            currSpawnCd -= Time.deltaTime;
        }
        
        
    }
}
