using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScr : MonoBehaviour
{
    public float timerDN = 0f;
    public Transform[] EnemyLoc;
    public Transform[] ResourceLoc;
    public GameObject enemyPrefab;
    public bool isNight = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerDN -= Time.deltaTime;
        if(timerDN <= 0)
        {
            spawnEnemies();
            isNight = true;
            timerDN = 300f;
        }
    }

    void spawnEnemies()
    {
        print("spawning enemies");
        foreach(Transform location in EnemyLoc)
        {
            float randnum = Random.Range(0, 2);
            if(randnum >= 1)
            {
                Instantiate(enemyPrefab, location);
            }
        }
    }
}
