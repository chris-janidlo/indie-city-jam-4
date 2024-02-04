using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PipeTile : MonoBehaviour
{
    public List<Vector2Int> connectingDirections;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [NonSerialized]
    public bool ConnectedToHeart;

    [NonSerialized]
    public Vector2Int Position;

    void Awake()
    {
        Position = Vector2Int.RoundToInt(transform.position);
    }

    void Update()
    {
        spriteRenderer.color = ConnectedToHeart ? Color.red : Color.white;
    }
}