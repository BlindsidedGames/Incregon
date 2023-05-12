using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using static Oracle;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private TileManager[] worldTiles;

    private void Awake()
    {
        for (var i = 0; i < worldTiles.Length; i++) worldTiles[i].tileID = worldTiles[i].tileID = i;
    }

    [Button]
    public void AddTilesToArray()
    {
        worldTiles = GetComponentsInChildren<TileManager>();
    }
}