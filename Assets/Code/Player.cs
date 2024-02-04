using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PipeTile Tile;
    public Vector2Int Direction;

    [SerializeField] private Ease moveEase;
    [SerializeField] private float moveTime;

    [SerializeField] private float lethargicAnimationSpeed;
    [SerializeField] private Color lethargicColor;
    
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    
    void Start()
    {
        rigidbody.position = Tile.transform.position;
    }

    void Update()
    {
        animator.speed = Tile.ConnectedToHeart ? 1 : lethargicAnimationSpeed;
        spriteRenderer.color = Tile.ConnectedToHeart ? Color.white : lethargicColor;
        Debug.DrawLine(transform.position, transform.position + new Vector3(Direction.x, Direction.y), Color.green);
    }

    public void OnHeartBeat()
    {
        Move();
    }

    private void Move()
    {
        Debug.Log("move");
        PipeTile next = null;
        if (Tile.CurrentAdjacencies.TryGetValue(Direction, out var straight) && !straight.IsHeart)
        {
            next = straight;
        }

        if (next == null)
        {
            foreach (var (dir, adj) in Tile.CurrentAdjacencies)
            {
                Debug.Log($"{dir} {adj}");
                if (adj.IsHeart || dir == Direction || dir == -Direction) continue;

                next = adj;
                Direction = dir;
                break;
            }
        }

        if (next == null && Tile.CurrentAdjacencies.TryGetValue(-Direction, out var back) && !back.IsHeart)
        {
            next = back;
            Direction = -Direction;
        }
        
        if (next == null) return;

        Tween.RigidbodyMovePosition(rigidbody, next.transform.position, moveTime, moveEase);
        // transform.up = new Vector3(Direction.x, Direction.y);
        Tile = next;
    }
}
