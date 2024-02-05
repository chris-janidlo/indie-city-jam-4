using System;
using System.Collections;
using System.Collections.Generic;
using crass;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private Vector2 spawnRadii;
    [SerializeField] private int beatsPerSpawn, beatResetAfterLastEnemyDies;
    [SerializeField] private Enemy enemyPrefab;

    private int enemyCount;
    private int beatCounter = 1;

    void Awake()
    {
        SingletonSetInstance(this, true);
    }

    public void OnHeartBeat()
    {
        if (--beatCounter <= 0)
        {
            beatCounter = beatsPerSpawn;
            Spawn();
        }
    }

    public void NotifyEnemyDied()
    {
        if (--enemyCount <= 0)
        {
            enemyCount = 0; // paranoid
            beatCounter = beatResetAfterLastEnemyDies;
        }
    }

    private void Spawn()
    {
        var pos = Random.insideUnitCircle.normalized * Random.Range(spawnRadii.x, spawnRadii.y);
        var roundPos = Vector2Int.RoundToInt(pos);
        Instantiate(enemyPrefab, new Vector3(roundPos.x, roundPos.y), Quaternion.identity);
        enemyCount++;
    }
}
