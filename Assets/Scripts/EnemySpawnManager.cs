﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject VoodoDude;
    public Transform Target;
    public float rndSpawnRange = 2;

    List<GameObject> enemies;

    //COUNTERS
    public float timeOfSpawnDefault = 5f;
    float timeOfSpawnCounter;

    void Start()
    {
        enemies = new List<GameObject>();

        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(VoodoDude, transform);
            Vector3 rnd = new Vector3(Random.Range(-rndSpawnRange, rndSpawnRange), 0, Random.Range(-rndSpawnRange, rndSpawnRange));
            go.transform.position = transform.position + rnd;
            EnemyBehaviour enemyBehaviour = go.GetComponent<EnemyBehaviour>();
            enemyBehaviour.Player = Target;
            enemyBehaviour.InitialPosition = go.transform.position;
            go.SetActive(false);
            enemies.Add(go);
        }
    }

    void Update()
    {
        SetState();
    }

    void SetState()
    {
        switch (GameMgr.GameState)
        {
            case GameProgress.Start:

                break;

            case GameProgress.Early:
                earlyGameState();
                break;

            case GameProgress.Mid:
                midGameState();
                break;

            case GameProgress.Late:
                lateGameState();
                break;

            default:
                break;
        }
    }

    void startGameState()
    {
        timeOfSpawnCounter += Time.deltaTime;
        if (timeOfSpawnCounter >= timeOfSpawnDefault)
        {
            timeOfSpawnCounter = 0;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeSelf)
                {
                    enemies[i].SetActive(true);
                    break;
                }
            }
        }
    }

    void earlyGameState()
    {
        timeOfSpawnCounter += Time.deltaTime;
        if (timeOfSpawnCounter >= timeOfSpawnDefault)
        {
            timeOfSpawnCounter = 0;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeSelf)
                {
                    enemies[i].SetActive(true);
                    break;
                }
            }
        }
    }

    void midGameState()
    {
        timeOfSpawnCounter += Time.deltaTime;
        if (timeOfSpawnCounter >= timeOfSpawnDefault)
        {
            timeOfSpawnCounter = 0;
            int index = 0;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeSelf)
                {
                    enemies[i].SetActive(true);
                    index++;
                    if(index == 2)
                        break;
                }
            }
        }
    }

    void lateGameState()
    {
        timeOfSpawnCounter += Time.deltaTime;
        if (timeOfSpawnCounter >= timeOfSpawnDefault *0.5f)
        {
            timeOfSpawnCounter = 0;
            int index = 0;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeSelf)
                {
                    enemies[i].SetActive(true);
                    index++;
                    if (index == 2)
                        break;
                }
            }
        }
    }
}
