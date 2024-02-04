using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class PipeGrid : crass.Singleton<PipeGrid>
{
    [SerializeField] private PipeTile heart;
    [SerializeField]
    private List<PipeTile> initialTiles;
    
    [NonSerialized]
    public Dictionary<Vector2Int, PipeTile> Tiles;

    private void Start()
    {
        Tiles = new();
        foreach (var tile in initialTiles)
        {
            Tiles[tile.Position] = tile;
        }

        Tiles[heart.Position] = heart;
        
        UpdateHeartConnections();
    }

    public void UpdateHeartConnections()
    {
        foreach (var tile in Tiles.Values)
            tile.ConnectedToHeart = false;

        var tested = new HashSet<PipeTile>();

        void propagateConnection(PipeTile tile)
        {
            tile.ConnectedToHeart = true;
            foreach (var dir in tile.connectingDirections)
            {
                // TODO account for rotation
                if (!Tiles.TryGetValue(tile.Position + dir, out var adjTile)) continue;
                
                // ReSharper disable once CanSimplifySetAddingWithSingleCall
                if (tested.Contains(adjTile)) continue;

                tested.Add(adjTile);
                propagateConnection(adjTile);
            }
        }
        
        propagateConnection(heart);
    }
}
