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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cell = tilemap.WorldToCell(worldPosition);

            Tile tile = tilemap.GetTile<Tile>(cell);
            if (tile == wallTile || tile == destructibleTile) return;

            Vector3 cellCenterWorld = tilemap.GetCellCenterWorld(cell);
            Instantiate(bombPrefab, cellCenterWorld, Quaternion.identity);
        }
    }

    public void Spawn() {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(gameObject.transform.position);
        Vector3Int cell = tilemap.WorldToCell(transform.position);

        Tile tile = tilemap.GetTile<Tile>(cell);
        if (tile == wallTile || tile == destructibleTile) return;

        Vector3 cellCenterWorld = tilemap.GetCellCenterWorld(cell);
        Instantiate(bombPrefab, cellCenterWorld, Quaternion.identity);
    }
}
