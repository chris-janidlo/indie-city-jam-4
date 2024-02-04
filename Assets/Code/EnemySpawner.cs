using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnRadii;
    [SerializeField] private int beatsPerSpawn;
    [SerializeField] private Enemy enemyPrefab;

    private int beatCounter = 1;

    public void OnHeartBeat()
    {
        if (--beatCounter <= 0)
        {
            beatCounter = beatsPerSpawn;
            Spawn();
        }
    }

    private void Spawn()
    {
        var pos = Random.insideUnitCircle * Random.Range(spawnRadii.x, spawnRadii.y);
        var roundPos = Vector2Int.RoundToInt(pos);
        Instantiate(enemyPrefab, new Vector3(roundPos.x, roundPos.y), Quaternion.identity);
    }
}
