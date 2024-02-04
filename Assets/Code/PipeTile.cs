using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using crass;
using PrimeTween;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PipeTile : MonoBehaviour
{
    public List<Vector2Int> connectingDirections;

    [SerializeField] private Ease rotateEase;
    [SerializeField] private float rotateTime;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color nonConnectedColor;
    [SerializeField] private List<Sprite> possibleSprites;
    
    [NonSerialized]
    public bool ConnectedToHeart;

    [NonSerialized]
    public Vector2Int Position;

    [NonSerialized] public Dictionary<Vector2Int, PipeTile> CurrentAdjacencies = new();

    [NonSerialized] public bool IsHeart;

    private bool rotating;

    void Awake()
    {
        Position = Vector2Int.RoundToInt(transform.position);
        if (possibleSprites.Count > 0) spriteRenderer.sprite = possibleSprites.PickRandom();
    }

    void Update()
    {
        spriteRenderer.color = ConnectedToHeart ? Color.white : nonConnectedColor;
        
        foreach (var dir in RotatedDirections())
        {
            Debug.DrawLine(transform.position, transform.position + new Vector3(dir.x/4f, dir.y/4f), Color.blue);
        }
    }

    void OnDrawGizmos()
    {
        Handles.Label(transform.position, name);
    }

    public List<Vector2Int> RotatedDirections()
    {
        return connectingDirections
            .Select(dir => Vector2Int.RoundToInt(transform.rotation * new Vector3(dir.x, dir.y)))
            .ToList();
    }

    IEnumerator OnMouseUpAsButton()
    {
        if (IsHeart || rotating) yield break;
        rotating = true;
        CurrentAdjacencies.Clear();
        yield return Tween.Rotation(
            transform,
            endValue: transform.rotation * Quaternion.Euler(0, 0, 90),
            ease: rotateEase,
            duration: rotateTime
        ).ToYieldInstruction();
        PipeGrid.Instance.UpdateHeartConnections();
        rotating = false;
    }
}