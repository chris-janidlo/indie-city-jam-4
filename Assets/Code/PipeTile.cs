using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;

public class PipeTile : MonoBehaviour
{
    public List<Vector2Int> connectingDirections;

    [SerializeField] private Ease rotateEase;
    [SerializeField] private float rotateTime;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [NonSerialized]
    public bool ConnectedToHeart;

    [NonSerialized]
    public Vector2Int Position;

    [NonSerialized] public bool IsHeart;

    private bool rotating;

    void Awake()
    {
        Position = Vector2Int.RoundToInt(transform.position);
    }

    void Update()
    {
        spriteRenderer.color = ConnectedToHeart ? Color.red : Color.white;
    }

    IEnumerator OnMouseUpAsButton()
    {
        if (IsHeart || rotating) yield break;
        rotating = true;
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