using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile destructibleTile;
    public GameObject bombPrefab;

    public void Spawn() {
        Vector3Int cell = GetCell();

        Tile tile = tilemap.GetTile<Tile>(cell);
        if (tile == wallTile || tile == destructibleTile) return;

        Vector3 cellCenterWorld = tilemap.GetCellCenterWorld(cell);
        GameObject bomb = Instantiate(bombPrefab, cellCenterWorld, Quaternion.identity);
        bomb.GetComponent<Bomb>().SetSpawnerPlayer(cell, gameObject);
    }

    public Vector3Int GetCell() {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(gameObject.transform.position);
        return tilemap.WorldToCell(transform.position);
    }
}
