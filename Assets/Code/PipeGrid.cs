using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class PipeGrid : crass.Singleton<PipeGrid>
{
    [SerializeField] private PipeTile heart;
    
    [NonSerialized]
    public Dictionary<Vector2Int, PipeTile> Tiles;

    private void Awake()
    {
        SingletonSetInstance(this, true);
    }

    private void Start()
    {
        Tiles = new();
        foreach (var tile in GetComponentsInChildren<PipeTile>())
        {
            Tiles[tile.Position] = tile;
        }

        heart.IsHeart = true;
        Tiles[heart.Position] = heart;
        
        UpdateHeartConnections();
    }

    public void UpdateHeartConnections()
    {
        foreach (var tile in Tiles.Values)
        {
            tile.CurrentAdjacencies.Clear();
            tile.ConnectedToHeart = false;
        }

        var tested = new HashSet<PipeTile>();

        void propagateConnection(PipeTile tile)
        {
            tile.ConnectedToHeart = true;
            tested.Add(tile);
            foreach (var dir in tile.RotatedDirections())
            {
                if (!Tiles.TryGetValue(tile.Position + dir, out var adjTile)) continue;

                if (tested.Contains(adjTile)) continue;
                
                if (!adjTile.RotatedDirections().Contains(-dir)) continue;

                tile.CurrentAdjacencies[dir] = adjTile;
                adjTile.CurrentAdjacencies[-dir] = tile; 
                propagateConnection(adjTile);
            }
        }
        
        propagateConnection(heart);
    }
}
