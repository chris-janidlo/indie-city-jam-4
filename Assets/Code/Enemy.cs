using System;
using System.Collections;
using System.Collections.Generic;
using crass;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = Unity.Mathematics.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int beatsPerMove;
    [SerializeField] private new Rigidbody2D rigidbody;
    
    private int beatCounter;

    void Update()
    {
        if (rigidbody.position == Vector2.zero) SceneManager.LoadScene("Game Over");
    }

    void OnEnable()
    {
        Heart.Instance.OnHeartBeat.AddListener(OnHeartBeat);
    }

    void OnDisable()
    {
        Heart.Instance.OnHeartBeat.RemoveListener(OnHeartBeat);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            EnemySpawner.Instance.NotifyEnemyDied();
            Destroy(gameObject);
        }
    }

    private void OnHeartBeat()
    {
        if (++beatCounter >= beatsPerMove)
        {
            beatCounter = 0;
            MoveTowardHeart();
        }
    }

    private void MoveTowardHeart()
    {
        // TODO animate
        // assumes heart is at origin

        var dir = new Vector2Int(-Math.Sign(transform.position.x), -Math.Sign(transform.position.y));

        if (dir.x == 0 || dir.y == 0)
        {
            rigidbody.position += dir;
            return;
        }

        var randDir = RandomExtra.Chance(0.5f) ? Vector2Int.right * dir.x : Vector2Int.up * dir.y;

        rigidbody.position += randDir;
    }
}
